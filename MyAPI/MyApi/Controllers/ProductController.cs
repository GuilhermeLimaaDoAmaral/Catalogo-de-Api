using ApiCatalog.Core.DTOs.Request;
using ApiCatalog.Core.DTOs.Response;
using ApiCatalog.Core.Entities;
using ApiCatalog.Core.Interfaces.Repository;
using ApiCatalog.Core.Pagination;
using ApiCatalog.ResponseBuilder;
using ApiCatalog.Utils;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalog.Controllers
{
    [Route("/api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductRequestDTO productDTO)
        {

            var productCreated = _unitOfWork.ProductRepository.Create(_mapper.Map<Product>(productDTO));

            _unitOfWork.Commit();

            return Ok(new ResponseModelBuilder().WithMessage("Produto criado com sucesso!")
                                                .WithSuccess(true)
                                                .WithData(_mapper.Map<ProductResponseDTO>(productCreated))
                                                .WithAlert(AlertType.Success)
                                                .Build());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product != null)
            {
                return Ok(new ResponseModelBuilder().WithMessage("Busca pelo produto realizada com sucesso")
                                                .WithSuccess(true)
                                                .WithData(product)
                                                .WithAlert(AlertType.Success)
                                                .Build());
            }
            else
            {
                return NotFound(new ResponseModelBuilder().WithMessage("Produto não encontrado na base de dados")
                                                        .WithSuccess(false)
                                                        .WithData(id)
                                                        .WithAlert(AlertType.Error)
                                                        .Build());
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (products != null)
            {
                return Ok(new ResponseModelBuilder().WithMessage("Busca pelos produtos realizada com sucesso")
                                               .WithSuccess(true)
                                               .WithData(products)
                                               .WithAlert(AlertType.Success)
                                               .Build());
            }
            else
            {
                return NotFound(new ResponseModelBuilder().WithMessage("Produtos não encontrado na base de dados")
                                                       .WithSuccess(false)
                                                       .WithAlert(AlertType.Error)
                                                       .Build());
            }
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProductResponseDTO>> Get([FromQuery]
                                                                PaginationBase<Product> PaginationProduct)
        {
            var products = _unitOfWork.ProductRepository.GetAllPagination(PaginationProduct);

            if (products != null && products.Any())
            {
                var metaData = new
                {
                    products.TotalCount,
                    products.PageSize,
                    products.CurrentPage,
                    products.TotalPage,
                    products.HasNext,
                    products.HasPrevious,
                };

                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));


                return Ok(new ResponseModelBuilder().WithMessage("Busca pelos produtos realizada com sucesso")
                                               .WithSuccess(true)
                                               .WithData(_mapper.Map<IEnumerable<ProductResponseDTO>>(products))
                                               .WithAlert(AlertType.Success)
                                               .Build());
            }
            else
            {
                return NotFound(new ResponseModelBuilder().WithMessage("Produtos não encontrado na base de dados")
                                                       .WithSuccess(false)
                                                       .WithAlert(AlertType.Error)
                                                       .Build());
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] ProductRequestDTO productDTO)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product != null)
            {
               var productUpdate = _unitOfWork.ProductRepository.Update(_mapper.Map<Product>(productDTO));

                _unitOfWork.Commit();

                return Ok(new ResponseModelBuilder().WithMessage("Produto atualizado com sucesso!")
                                                     .WithSuccess(true)
                                                     .WithData(productUpdate)
                                                     .WithAlert(AlertType.Success)
                                                     .Build());
            }
            else
            {
                return NotFound(new ResponseModelBuilder().WithMessage("Produto não encontrado na base de dados")
                                                           .WithSuccess(false)
                                                           .WithData(id)
                                                           .WithAlert(AlertType.Error)
                                                           .Build());
            }
        }

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProductResponseDTO> Patch(int id, JsonPatchDocument<ProductRequestDTO> patchProductDTO)
        {
            if (patchProductDTO == null || IDHelper.IsIdInvalid(id))
                BadRequest(new ResponseModelBuilder().WithMessage("Erro ao atualizar produto")
                                                       .WithSuccess(false)
                                                       .WithData(id)
                                                       .WithAlert(AlertType.Error)
                                                       .Build());

            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product == null)
                NotFound(new ResponseModelBuilder().WithMessage("Produto não encontrado na base de dados")
                                                       .WithSuccess(false)
                                                       .WithData(id)
                                                       .WithAlert(AlertType.Error)
                                                       .Build());

            var productRequest = _mapper.Map<ProductRequestDTO>(product);

            patchProductDTO.ApplyTo(productRequest, ModelState);

            if (ModelState.IsValid || TryValidateModel(productRequest))
                BadRequest(ModelState);


            product = _unitOfWork.ProductRepository.Update(_mapper.Map<Product>(productRequest));

            _unitOfWork.Commit();

            return Ok(new ResponseModelBuilder().WithMessage("Produto atualizado com sucesso!")
                                                .WithSuccess(true)
                                                .WithData(_mapper.Map<ProductResponseDTO>(product))
                                                .WithAlert(AlertType.Success)
                                                .Build());

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product != null)
            {
                _unitOfWork.ProductRepository.Delete(id);
                _unitOfWork.Commit();
                return Ok(new ResponseModelBuilder().WithMessage("Produto excluido com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(id)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            else
            {
                return NotFound(new ResponseModelBuilder().WithMessage("Produto não encontrado na base de dados")
                                                       .WithSuccess(false)
                                                       .WithData(id)
                                                       .WithAlert(AlertType.Error)
                                                       .Build());
            }
        }
    }
}
