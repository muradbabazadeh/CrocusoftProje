using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Newtonsoft.Json;

namespace CrocusoftProje
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.Error = (_, args) =>
                    {
                        Console.WriteLine(args.ErrorContext.Error);
                    };

                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
                );
            var settings = Configuration.Get<Infrastructure.CrocusoftProjeSettings>();
            services.Configure<Infrastructure.CrocusoftProjeSettings>(Configuration);
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(Api.Infrastructure.ErrorHandling.HttpGlobalExceptionFilter));
            })
                .AddControllersAsServices();

            services.AddEntityFrameworkSqlServer()
               .AddDbContext<Infrastructure.Database.CrocusoftProjeDbContext>(options =>
               {
                   options.UseSqlServer(settings.ConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly("Parla.Infrastructure.Migrations");
                            sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(3), new List<int>());
                        });
               }
               );

            ConfigureSwagger(services);

            ConfigureJwtAuthentication(services, settings);


            // Add application services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions();

            //configure Autofac
            var container = new Autofac.ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new Api.Infrastructure.AutofacModules.MediatorModule());
            container.RegisterModule(new Api.Infrastructure.AutofacModules.ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", true, true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMiddleware<Api.Infrastructure.Middlewares.AuthorizationErrorMiddleware>();

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parla.API V1");

                  c.DocumentTitle = "Parla API";
                  c.DocExpansion(DocExpansion.List);
              });

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


        #region HelperMethods


        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Parla API",
                    Version = "v1",
                    Description = "The Parla CRM Service API"
                });

                options.OperationFilter<Api.Infrastructure.Filters.AuthorizeCheckOperationFilter>();

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
            });
        }

        private void ConfigureJwtAuthentication(IServiceCollection services, Infrastructure.CrocusoftProjeSettings settings)
        {
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });
             
        }
        #endregion

    }
}
