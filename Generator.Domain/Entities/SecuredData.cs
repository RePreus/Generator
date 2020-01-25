using System;
using Generator.Domain.Abstractions;

namespace Generator.Domain.Entities
{
    public class SecuredData : Entity
    {
        public SecuredData(string data, string token, Guid userId)
        {
            Data = data;
            Token = token;
            UserId = userId;
        }

        public string Data { get; private set; }

        public string Token { get; private set; }

        public Guid UserId { get; private set; }
    }
}
