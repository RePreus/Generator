using System.Collections.Generic;

namespace Generator.Application.Models
{
    public class RequestResult
    {
        public RequestResult(List<string> message = null, bool success = true)
        {
            Success = success;
            Message = message;
        }

        public RequestResult(string message, bool success = true)
        {
            Success = success;
            Message = new List<string> { message };
        }

        public bool Success { get; private set; }

        public List<string> Message { get; private set; }
    }
}
