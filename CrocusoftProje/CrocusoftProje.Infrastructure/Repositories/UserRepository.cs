using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Infrastructure.Database;
using CrocusoftProje.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public sealed override DbContext Context { get; protected set; }

        public UserRepository(CrocusoftProjeDbContext context)
        {
            Context = context;
        }
    }
}
