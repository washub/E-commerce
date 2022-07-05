using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams prodParams):base(
            x => 
            (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search))
            &&(!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId) 
            && (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId)
        )
        {
        }
    }
}