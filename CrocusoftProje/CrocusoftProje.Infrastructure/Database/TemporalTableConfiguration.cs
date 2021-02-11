using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Infrastructure.Database
{
    public class TemporalTableConfiguration
    {
        private readonly List<Type> _temporalTypes = new List<Type>();
        public IReadOnlyCollection<Type> TemporalTypes => _temporalTypes;

        public TemporalTableConfiguration()
        {

        }

        private void RegisterTemporalTable<TEntity>()
            where TEntity : Entity
        {
            _temporalTypes.Add(typeof(TEntity));
        }
    }
}
