using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.Infrastructure.Database;
using CrocusoftProje.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public sealed override DbContext Context { get; protected set; }

        public RoleRepository(CrocusoftProjeDbContext context)
        {
            Context = context;
        }
    }
}
