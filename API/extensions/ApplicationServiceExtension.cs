using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services){

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //always put after controllers to override api controller
            services.Configure<ApiBehaviorOptions>(options =>{
                options.InvalidModelStateResponseFactory = actionContext =>{
                    var errors = actionContext.ModelState.Where(q => q.Value.Errors.Count>0)
                    .SelectMany(s => s.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new APIValidationErrorResponse{
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);

                };
            });
            return services;
        }
    }
}