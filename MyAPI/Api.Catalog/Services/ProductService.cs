using ApiCatalog.Core.Interfaces.Repository;
using ApiCatalog.Core.Interfaces.Services;
namespace ApiCatalog.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
