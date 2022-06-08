using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo){
            _repo = repo;

        }
           
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var prod =  await _repo.GetProductsListAsync();
            return Ok(prod);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            var prod = await _repo.GetProductByIdAsync(id);
            if(prod == null){
                return NotFound(new Product{Name="No data found"});
            }
            return Ok(prod);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands(){
            var brands = await _repo.GetProductBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes(){
            var types = await _repo.GetProductTypesAsync();
            return Ok(types);
        }
    }
}