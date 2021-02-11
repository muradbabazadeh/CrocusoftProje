using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.SharedKernel.Domain.Seedwork
{
    public interface IAggregateRoot
    {
        int Id { get; set; }
    }
}
