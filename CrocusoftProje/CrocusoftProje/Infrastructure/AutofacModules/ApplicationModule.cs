using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Identity;
using CrocusoftProje.Identity.Auth;
using CrocusoftProje.Infrastructure;
using CrocusoftProje.Infrastructure.Idempotency;
using CrocusoftProje.Infrastructure.Identity;
using CrocusoftProje.Infrastructure.Repositories;
using CrocusoftProje.UserDetails;

namespace CrocusoftProje.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            AutofacHelper.RegisterCqrsTypes<IdentityDetails>(builder);
            AutofacHelper.RegisterCqrsTypes<UserDetailsModule>(builder);
            AutofacHelper.RegisterCqrsTypes<ApplicationModule>(builder);





            AutofacHelper.RegisterAutoMapperProfiles<IdentityDetails>(builder);
            AutofacHelper.RegisterAutoMapperProfiles<ApplicationModule>(builder);
            AutofacHelper.RegisterAutoMapperProfiles<UserDetailsModule>(builder);





            // Repositories
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClaimsManager>()
                .As<IClaimsManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserManager>()
                .As<IUserManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleRepository>()
              .As<IRoleRepository>()
              .InstancePerLifetimeScope();
        }


    }
}
