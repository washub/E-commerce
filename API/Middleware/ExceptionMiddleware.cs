using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context){
            try{
                    await _next(context);

            }
            catch(Exception ex){

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                //check if we are in developement mode then send dev error or custome error
                var response = _env.IsDevelopment()
                ? new ApiExceptions((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ApiExceptions((int) HttpStatusCode.InternalServerError);

                // serialize our response object to json and use camelCase policy in the response
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                // write the json back to our http response
                await context.Response.WriteAsync(json);
            }
        }
    }
}