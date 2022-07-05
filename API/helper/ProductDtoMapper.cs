
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.helper
{
    public class ProductDtoMapper : Profile
    {
        public ProductDtoMapper()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(p => p.ProductBrand, o=> o.MapFrom(m => m.ProductBrand.Name))
            .ForMember(p=> p.ProductType, o=> o.MapFrom(m=> m.ProductType.Name))
            .ForMember(p=> p.PictureUrl, o=> o.ResolveUsing<ProductUrlResolver>());
        }
    }
}