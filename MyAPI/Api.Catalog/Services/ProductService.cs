using ApiCatalog.Core.DTOs;
using ApiCatalog.Core.DTOs.Request;
using ApiCatalog.Core.Entities;
using ApiCatalog.Core.Interfaces.Repository;
using ApiCatalog.Core.Interfaces.Services;
using ApiCatalog.Core.Models;
using ApiCatalog.Persistence.Repository;
using ApiCatalog.Utils;
using System.Transactions;

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
