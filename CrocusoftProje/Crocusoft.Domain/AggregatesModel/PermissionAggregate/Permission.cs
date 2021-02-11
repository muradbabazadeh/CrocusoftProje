﻿using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.PermissionAggregate
{
    public class Permission : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        private readonly List<PermissionParameter> _parameters;
        public IReadOnlyCollection<PermissionParameter> Parameters => _parameters;

        public Permission()
        {
            _parameters = new List<PermissionParameter>();
        }

        public void SetDetails(PermissionCode code, string description)
        {
            Id = code.Id;
            Name = code.Name;
            Description = description;
        }

        public void AddParameter(PermissionParameter parameter)
        {
            _parameters.Add(parameter);
        }
    }
}
