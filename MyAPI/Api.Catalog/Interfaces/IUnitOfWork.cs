using ApiCatalog.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Persistence.Repository
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        void Commit();
    }
}
