using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataBase
{
    public interface IMarDevRepository<T>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Find(Expression<Func<T, bool>> predicate=null);

        void SaveChanges();
    }
}
