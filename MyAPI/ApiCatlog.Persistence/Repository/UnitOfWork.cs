using ApiCatalog.Core.Interfaces.Repository;
using ApiCatalog.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProductRepository? _productRepository;

        public ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository = _productRepository ?? new ProductRepository(_applicationDbContext);
            }
        }

        public void Commit()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Dispose() 
        {
            _applicationDbContext.Dispose();
        }
    }
}
