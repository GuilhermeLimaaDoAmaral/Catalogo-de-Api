using ApiCatalog.Core.Entities;
using ApiCatalog.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Core.Interfaces.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<Product> GetProducts(ProductParameters productParameters);
    }
}
