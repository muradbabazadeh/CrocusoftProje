using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrocusoftProje.Infrastructure.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid key);

        Task CreateRequestForCommandAsync<T>(Guid key);
    }
}
