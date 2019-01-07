using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    //public class DataBaseWork : IDataBaseWork
    //{
    //    private readonly mardevContext _context;
    //    private Hashtable _repositories;
    //    private bool disposedValue = false; // To detect redundant calls
    //    public DataBaseWork(mardevContext context)
    //    {
    //        _context = context;
    //    }
    //    public IMarDevRepository<T> MarDevRepository<T>() where T : class
    //    {
    //        if (_repositories == null)
    //        {
    //            _repositories = new Hashtable();
    //        }

    //        var type = typeof(T).Name;

    //        if (!_repositories.ContainsKey(type))
    //        {
    //            var repositoryType = typeof(MarDevRepository<>);

    //            var repositoryInstance =
    //                Activator.CreateInstance(repositoryType
    //                        .MakeGenericType(typeof(T)), _context);

    //            _repositories.Add(type, repositoryInstance);
    //        }

    //        return (IMarDevRepository<T>)_repositories[type];
    //    }

    //    public void Save()
    //    {
    //        _context.SaveChanges();
    //    }

    //    #region IDisposable Support     
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!disposedValue)
    //        {
    //            if (disposing)
    //            {
    //                _context.Dispose();
    //            }
    //            disposedValue = true;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //    }
    //    #endregion
    //}
}
