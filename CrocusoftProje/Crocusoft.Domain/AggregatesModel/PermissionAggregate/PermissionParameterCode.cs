using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.PermissionAggregate
{
    public class PermissionParameterCode : Enumeration
    {
        public static PermissionParameterCode PharmaViewScope = new PermissionParameterCode(1, PermissionParameterName.Scope);
        public PermissionParameterCode(int id, string name) : base(id, name)
        {
        }

        public PermissionParameterCode() { }
    }
}
