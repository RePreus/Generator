using System.Security.Cryptography;
using Generator.Domain.Abstractions;

namespace Generator.Application.Security
{
    public class SecuredData : Entity
    {
        public SecuredData(string data, string token)
        {
            Data = data;
            Token = token;
        }

        public string Data { get; set; }

        public string Token { get; set; }
    }
}
