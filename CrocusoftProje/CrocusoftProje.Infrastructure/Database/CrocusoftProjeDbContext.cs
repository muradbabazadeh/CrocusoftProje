using CrocusoftProje.SharedKernel.Domain.Seedwork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrocusoftProje.SharedKernel.Infrastructure;
using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.Domain.AggregatesModel.PermissionAggregate;

namespace CrocusoftProje.Infrastructure.Database
{
   public class CrocusoftProjeDbContext : DbContext, IUnitOfWork
    {
        public const string IDENTITY_SCHEMA = "Identity";
        public const string DEFAULT_SCHEMA = "dbo";

        private readonly IMediator _mediator;

        public CrocusoftProjeDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<UserRole> UserRoles { get; private set; }
        public DbSet<UserPermission> UserPermisson { get; private set; }
        public DbSet<RolePermission> RolePermissons { get; private set; }
        public DbSet<Permission> Permissions { get; private set; }



        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await SaveChangesAsync(true, cancellationToken);

            return true;
        }

    }
}
