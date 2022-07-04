using System;
using System.Collections.Generic;

namespace API.Errors
{
    public class APIValidationErrorResponse : ApiErrorResponse
    {
        public APIValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<String> Errors { get; set; }
    }
}