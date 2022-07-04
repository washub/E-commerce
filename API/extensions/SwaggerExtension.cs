using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services){
            services.AddSwaggerGen(c=>{
                c.SwaggerDoc("v1", new OpenApiInfo{Title = "E-commerceAPI", Version = "v1.0"});
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app){
            app.UseSwagger();
                app.UseSwaggerUI(c=>{
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Comm API v1.0");
                });

            return app;
        }
    }

   
}