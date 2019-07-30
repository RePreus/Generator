using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;

namespace Generator.Application.Models
{
    public class RequestResult
    {
        public RequestResult(List<string> message = null, bool success = true)
        {
            this.Success = success;
            this.Message = message;
        }

        public RequestResult(string message, bool success = true)
        {
            this.Success = success;
            this.Message = new List<string>();
            this.Message.Add(message);
        }

        public bool Success { get; set; }

        public List<string> Message { get; set; }
    }
}
