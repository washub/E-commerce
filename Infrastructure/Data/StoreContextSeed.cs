using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory logger){
            try{
                if(!context.ProductBrands.Any()){
                    var data = File.ReadAllText("../Infrastructure/Data/DataSeed/brands.json");
                    var objs = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                    context.AddRange(objs);
                    await context.SaveChangesAsync();
                }
                if(!context.ProductTypes.Any()){    
                    var data = File.ReadAllText("../Infrastructure/Data/DataSeed/types.json");
                    var objs = JsonSerializer.Deserialize<List<ProductType>>(data);
                    context.AddRange(objs);
                    await context.SaveChangesAsync();
                }
                if(!context.Products.Any()){
                    var data = File.ReadAllText("../Infrastructure/Data/DataSeed/products.json");
                    var objs = JsonSerializer.Deserialize<List<Product>>(data);
                    context.AddRange(objs);
                    await context.SaveChangesAsync();
                }
            }catch(Exception ex){
                var log = logger.CreateLogger<StoreContextSeed>();
                log.LogError(ex.Message);
            }
        }
    }
}