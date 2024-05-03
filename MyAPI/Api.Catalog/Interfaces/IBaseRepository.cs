using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        T Create(T obj);
        T? GetById(long id);
        IEnumerable<T?> GetAll();
        T Update(T obj);
        void Delete(long id);
    }
}
