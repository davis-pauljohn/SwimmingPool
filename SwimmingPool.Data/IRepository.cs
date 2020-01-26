using SwimmingPool.Domain.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwimmingPool.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Add(T aggregate);

        void Update(T aggregate);

        T Find(Guid id);

        IQueryable<T> Query();
    }
}