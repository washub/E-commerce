using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string msg = null)
        {
            StatusCode = statusCode;
            Message = msg ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch{
                204 => "Sorry resource not found",
                400 => "Ooops a bad request you have made.",
                401 => "You are not authorized",
                404 => "Sorry resource not found",
                500 => "Seems like there is error at backend if not resolved please contact admin.",
                _ => null
            };
        }
    }
}