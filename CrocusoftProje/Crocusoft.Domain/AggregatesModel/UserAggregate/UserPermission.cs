using CrocusoftProje.Domain.AggregatesModel.PermissionAggregate;
using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.UserAggregate
{
    public class UserPermission : Entity
    {
        public int UserId { get; private set; }

        public int PermissionId { get; private set; }

        public Permission Permission { get; private set; }

        private readonly List<UserPermissionParameterValue> _parameterValues;
        public IReadOnlyCollection<UserPermissionParameterValue> ParameterValues => _parameterValues;

        public UserPermission()
        {
            _parameterValues = new List<UserPermissionParameterValue>();
        }

        public UserPermission(int permissionId) : this()
        {
            PermissionId = permissionId;
        }
        public UserPermission(int userId, int permissionId) : this(permissionId)
        {
            UserId = userId;
            PermissionId = permissionId;
        }
    }
}
