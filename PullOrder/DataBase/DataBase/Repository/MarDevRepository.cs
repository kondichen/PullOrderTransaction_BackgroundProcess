using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataBase
{
    //public class MarDevRepository<TEntity> : IMarDevRepository<TEntity>
    //    where TEntity : class
    //{
    //    private mardevContext Context { get; set; }

    //    public MarDevRepository(mardevContext context)
    //    {
    //        Context = context;
    //    }

    //    public void Create(TEntity entity)
    //    {
    //        Context.Set<TEntity>().Add(entity);
    //    }

    //    public void Delete(TEntity entity)
    //    {
    //        Context.Entry<TEntity>(entity).State = EntityState.Deleted;
    //    }

    //    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    //    {
    //        if (predicate != null)
    //            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
    //        else
    //            return Context.Set<TEntity>().FirstOrDefault();
    //    }

    //    public void Update(TEntity entity)
    //    {
    //        Context.Entry<TEntity>(entity).State = EntityState.Modified;
    //    }

    //    public int RetrieveBatchTransactionSeq(int x)
    //    {
    //        return x;
    //    }

    //    public void SaveChanges()
    //    {
    //        Context.SaveChanges();
    //    }
    //}
}
