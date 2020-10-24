using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.DTOs;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductType> productTypeRepo,
        IGenericRepository<ProductBand> productBrandRepo, IMapper  mapper)
        {
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            
            var products = await _productsRepo.ListAsync(spec); 
            return Ok(_mapper
            .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDTO>>(products));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);
           
            if(product == null) return NotFound(new ApiResponse(404));

            return  _mapper.Map<Product,ProductToReturnDTO>(product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
        {
            var productsTypes = await _productTypeRepo.ListAllAsync();
            return Ok(productsTypes);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBand>>> GetProductsBrands()
        {
            var productsBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productsBrands);
        }
    }
}