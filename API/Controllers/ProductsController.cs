using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            this._productBrandRepo = productBrandRepo;
            this._productRepo = productRepo;
            this._productTypeRepo = productTypeRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            return _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var prod = await _productRepo.GetEntityWithSpec(spec);
            
            return _mapper.Map<Product, ProductToReturnDto>(prod);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IList<ProductBrand>>> GetProductBrands()
        {
            return await _productBrandRepo.ListAllAsync();
        }


        [HttpGet("types")]
        public async Task<ActionResult<IList<ProductType>>> GetProductTypes()
        {
            return await _productTypeRepo.ListAllAsync();
        }
    }
}