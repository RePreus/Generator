using System.Collections.Generic;
using System.Net;

namespace Generator.Application.Models
{
    public class RequestResult
    {
        private bool success;
        private List<string> message;

        public RequestResult(bool success = true, List<string> message = null)
        {
            this.success = success;
            this.message = message;
        }
    }
}
