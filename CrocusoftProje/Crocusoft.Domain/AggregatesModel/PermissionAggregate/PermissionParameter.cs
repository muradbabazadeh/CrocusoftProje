using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.PermissionAggregate
{
    public class PermissionParameter : Entity
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Type { get; private set; }

        public string DefaultValue { get; private set; }

        public void SetDetails(PermissionCode code, string description, string type, string defaultValue)
        {
            Id = code.Id;
            Name = code.Name;
            Description = description;
            Type = type;
            DefaultValue = defaultValue;
        }
    }
}
