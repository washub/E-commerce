using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiExceptions : ApiErrorResponse
    {
        public ApiExceptions(int statusCode, string msg = null, string details = null) : base(statusCode, msg)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}