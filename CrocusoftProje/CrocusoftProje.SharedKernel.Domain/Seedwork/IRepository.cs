using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrocusoftProje.SharedKernel.Domain.Seedwork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> AddAsync(T entity);

        Task<T> GetAsync(int id);

        T UpdateAsync(T entity);

        bool DeleteAsync(T entity);
    }
}
