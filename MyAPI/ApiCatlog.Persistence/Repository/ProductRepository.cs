using ApiCatalog.Core.Entities;
using ApiCatalog.Core.Interfaces;
using ApiCatalog.Core.Interfaces.Repository;
using ApiCatalog.Pagination;
using ApiCatalog.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Persistence.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public IEnumerable<Product> GetProducts(ProductParameters productParameters)
        {
            return GetAll()
                      .OrderBy(x => x.Id)
                      .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                      .Take(productParameters.PageSize).ToList();
        }
    }
}
