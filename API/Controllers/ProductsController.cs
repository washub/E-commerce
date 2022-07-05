using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.helper;
using AutoMapper;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        #region Standalone-Controller
        // private readonly IProductRepository _repo;
        // private readonly IMapper _mapper;

        // public ProductsController(IProductRepository repo, IMapper mapper){
        //     _mapper = mapper;
        //     _repo = repo;

        // }
           
        // [HttpGet]
        // public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(){
        //     var prod =  await _repo.GetProductsListAsync();
        //     return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(prod));
        // }

        // [HttpGet("{id}")]
        // [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        // public async Task<ActionResult<Product>> GetProduct(int id){
        //     var prod = await _repo.GetProductByIdAsync(id);
        //     if(prod == null){
        //         return NotFound(new ApiErrorResponse(404));
        //     }
        //     return Ok(_mapper.Map<Product, ProductDto>(prod));
        // }

        // [HttpGet("brands")]
        // public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands(){
        //     var brands = await _repo.GetProductBrandsAsync();
        //     return Ok(brands);
        // }

        // [HttpGet("types")]
        // public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes(){
        //     var types = await _repo.GetProductTypesAsync();
        //     return Ok(types);
        // }
        #endregion
        #region Generic-Controller
        private readonly IGenericRepository<Product> _prodCont;
        private readonly IGenericRepository<ProductType> _typeCont;
        private readonly IGenericRepository<ProductBrand> _brandCont;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> prodCont, IGenericRepository<ProductType> typeCont, IGenericRepository<ProductBrand> brandCont, IMapper mapper)
        {
            _brandCont = brandCont;
            _mapper = mapper;
            _typeCont = typeCont;
            _prodCont = prodCont;
            
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productParams){

            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFilterForCountSpecification(productParams);

            var totalCount = await _prodCont.CountAsync(countSpec);

            var prod =  await _prodCont.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(prod);

            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalCount, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id){
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var prod = await _prodCont.GetEntityWithSpec(spec);
            if(prod == null){
                return BadRequest(new ApiErrorResponse(204));
            }
            return Ok(_mapper.Map<Product, ProductDto>(prod));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands(){
            var brands = await _brandCont.GetListAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes(){
            var types = await _typeCont.GetListAsync();
            return Ok(types);
        }
        #endregion Generic-Controller
    }
}