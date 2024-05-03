using ApiCatalog.Core.DTOs.Request;
using ApiCatalog.Core.DTOs.Response;
using ApiCatalog.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Core.DTOs.Automapper
{
    public class ProductDTOAutoMappingExtensionscs : Profile
    {
        public ProductDTOAutoMappingExtensionscs()
        {
            CreateMap<Product, ProductRequestDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<ProductRequestDTO, Product>().ReverseMap();
        }
    }
}
