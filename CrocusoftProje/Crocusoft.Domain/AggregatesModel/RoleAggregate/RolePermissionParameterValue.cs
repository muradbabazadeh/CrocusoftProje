using CrocusoftProje.Domain.AggregatesModel.PermissionAggregate;
using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.RoleAggregate
{
    public class RolePermissionParameterValue : Entity
    {
        public int PermissionParameterId { get; private set; }

        public int RolePermissionId { get; private set; }

        public string Value { get; private set; }

        public PermissionParameter PermissionParameter { get; private set; }

        public RolePermissionParameterValue()
        {
        }

        public RolePermissionParameterValue(int permissionParameterId, int rolePermissionId, string value) : this()
        {
            PermissionParameterId = permissionParameterId;
            RolePermissionId = rolePermissionId;
            Value = value;
        }
    }
}
