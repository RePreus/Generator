using System.Collections.Generic;

namespace Generator.Application.Models
{
    public class RequestResult
    {
        public RequestResult(IEnumerable<string> message = null, bool success = true)
        {
            Success = success;
            Message = message;
        }

        public RequestResult(string message, bool success = true)
            : this(new List<string> { message }, success)
        {
        }

        public bool Success { get; }

        public IEnumerable<string> Message { get; }
    }
}
