using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public interface IDataBaseWork : IDisposable
    {

        void Save();

        IMarDevRepository<T> MarDevRepository<T>() where T : class;
    }
}
