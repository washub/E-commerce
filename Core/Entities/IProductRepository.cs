using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface IProductRepository
    {
        public Task<Product> GetProductByIdAsync(int id);
        public Task<IReadOnlyList<Product>> GetProductsListAsync();
        public Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        public Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}