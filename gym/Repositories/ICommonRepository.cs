using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace gym.Repositories
{
    public interface ICommonRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int Id);
        IEnumerable<TEntity> GetAll();
        void Remove(TEntity entity);
        bool Save();
        void Update(TEntity entity);
    }
}