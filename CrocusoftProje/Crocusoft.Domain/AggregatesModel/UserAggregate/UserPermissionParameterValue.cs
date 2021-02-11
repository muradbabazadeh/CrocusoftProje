using CrocusoftProje.Domain.AggregatesModel.PermissionAggregate;
using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.UserAggregate
{
    public class UserPermissionParameterValue : Entity
    {
        public int PermissionParameterId { get; private set; }

        public string Value { get; private set; }

        public PermissionParameter PermissionParameter { get; private set; }

        public UserPermissionParameterValue()
        {
        }

        public UserPermissionParameterValue(int permissionParameterId, string value) : this()
        {
            PermissionParameterId = permissionParameterId;
            Value = value;
        }
    }
}
