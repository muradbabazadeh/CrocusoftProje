using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.PermissionAggregate
{
   public class PermissionCode : Enumeration
    {
        public PermissionCode(int id, string name) : base(id, name)
        {
        }

        public PermissionCode()
        {
        }
    }
}
