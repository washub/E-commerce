
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;
        
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
            
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl)){
                return _config["UrlAPI"] + source.PictureUrl;
            }
            return null;
        }
    }
}