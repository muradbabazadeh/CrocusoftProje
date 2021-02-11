﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrocusoftProje.Api.Extensions
{
    public static class WebHostExtensions
    {
        public static Microsoft.AspNetCore.Hosting.IWebHost MigrateDbContext<TContext>(this Microsoft.AspNetCore.Hosting.IWebHost webHost,
            Func<TContext, IServiceProvider, Task> seed) where TContext : Microsoft.EntityFrameworkCore.DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<Microsoft.Extensions.Logging.ILogger<TContext>>();

                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                    var retry = Polly.Policy.Handle<Microsoft.Data.SqlClient.SqlException>()
                        .WaitAndRetryAsync(new[]
                        {
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(10),
                            TimeSpan.FromSeconds(15)
                        });

                    retry.ExecuteAsync(async () =>
                    {
                        //if the sql server container is not created on run docker compose this
                        //migration can't fail for network related exception. The retry options for DbContext only 
                        //apply to transient exceptions.

                        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

                        if (pendingMigrations.Any())
                        {
                            await context.Database.MigrateAsync();
                        }

                        await seed(context, services);
                    }).Wait();


                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return webHost;
        }
    }
}
