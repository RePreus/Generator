using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;

namespace Generator.Application.Models
{
    public class RequestResult
    {
        public bool Success { get; set; }

        public List<string> Message { get; set; }

        public RequestResult(bool success = true, List<string> message = null)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
