using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams prodParams):base(
            x => 
            (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search))
            &&(!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId) 
            && (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId)
        )
        {

            AddInclude(x=>x.ProductBrand);
            
            AddInclude(x=>x.ProductType);

            AddOrderBy(x=> x.Name);

            AddPaging(prodParams.PageSize, prodParams.PageSize*(prodParams.PageIndex-1));

            if(!string.IsNullOrEmpty(prodParams.Sort)){
                switch(prodParams.Sort){
                    case "priceAsc" : AddOrderBy(x=> x.Price); break;
                    case "priceDesc": AddOrderByDescending(x=> x.Price); break;
                    default : AddOrderBy(x=> x.Name); break;
                }
            }
        }
        public ProductsWithTypesAndBrandsSpecification(int id):base(x=>x.Id == id)
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}