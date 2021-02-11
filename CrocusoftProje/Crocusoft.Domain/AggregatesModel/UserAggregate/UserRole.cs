using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.UserAggregate
{
    public class UserRole : Entity
    {
        public int UserId { get; private set; }

        public int? RoleId { get; private set; }

        public Role Role { get; private set; }

        public UserRole()
        {
        }

        public UserRole(int? roleId) : this()
        {
            RoleId = roleId;
        }

        public UserRole(int userId, int? roleId) : this(roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
