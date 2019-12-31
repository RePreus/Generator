using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Generator.Application.Persistence;

namespace Generator.Application.Security
{
    public abstract class SecurityToken
    {
        private string token;

        public async void SaveDataWithToken(IEnumerable<string> list, GeneratorContext context)
        {
            token = GenerateToken();
            var data = string.Join(";", list);
            await context.SecuredData.AddAsync(new SecuredData(data, token));
        }
        public async IEnumerable<string>
        private string GenerateToken()
        {
            var rand = new Random();
            var tmptoken = string.Empty;

            while (tmptoken.Length <= 24)
            {
                tmptoken += (char)rand.Next(33, 126);
            }

            return tmptoken;
        }
    }
}
