using System;
using System.Collections.Generic;
using System.Text;
using CrocusoftProje.Domain.AggregatesModel.UserAggregate;

namespace CrocusoftProje.Domain
{
    public class Editable<TUser> : Auditable<TUser> where TUser : User
    {
        public int? UpdatedById { get; protected set; }

        public DateTime? LastUpdateDateTime { get; protected set; }

        public TUser UpdatedBy { get; protected set; }

        public void SetAuditFields(int? updatedById, DateTime? lastUpdateDateTime)
        {
            UpdatedById = updatedById;
            LastUpdateDateTime = lastUpdateDateTime;
        }
    }
}
