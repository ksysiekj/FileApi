using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity, in TId>
        where TEntity : Entity<TId>
    {
        void Delete(TId id);
        void SaveOrUpdate(TEntity entity);
        TEntity Get(TId id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    }
}
