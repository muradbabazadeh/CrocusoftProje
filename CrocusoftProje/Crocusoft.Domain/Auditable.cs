using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain
{
    public class Auditable<TUser> : Entity where TUser : User
    {
        public int CreatedById { get; protected set; }

        public DateTime RecordDateTime { get; protected set; }

        public TUser CreatedBy { get; protected set; }

        public void SetAuditFields(int createdById)
        {
            if (CreatedById != 0 && CreatedById != createdById)
            {
                throw new ArgumentException("CreatedBy already set");
            }

            CreatedById = createdById;
            RecordDateTime = DateTime.Now;
        }
    }
}
