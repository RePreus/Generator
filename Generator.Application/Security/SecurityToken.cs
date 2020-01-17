using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generator.Application.Exceptions;
using Generator.Application.Persistence;
using Generator.Domain.Entities;

namespace Generator.Application.Security
{
    public class SecurityToken : ISecurityToken
    {
        private readonly GeneratorContext context;

        public SecurityToken(GeneratorContext context) => this.context = context;

        public async Task<string> SaveDataWithTokenAsync(IEnumerable<string> list, Guid userId)
        {
            if (context.SecuredData.Where(o => o.UserId == userId).ToList() is var removeList && removeList.Any())
                context.SecuredData.RemoveRange(removeList);

            var token = GenerateToken();
            var data = string.Join(";", list);
            await context.SecuredData.AddAsync(new SecuredData(data, token, userId));
            await context.SaveChangesAsync();
            return token;
        }

        public IList<string> GetSavedData(Guid userId, string token)
        {
            var securedData = context.SecuredData.Where(o => o.UserId == userId).ToList();

            if (securedData.Count < 1)
                throw new GeneratorException("User doesn't have any stored tokens");
            if (securedData.Count > 2)
                throw new GeneratorException("User has 2 or more stored tokens");

            if (token == securedData.First().Token)
                return securedData.First().Data.Split(';');

            throw new GeneratorException("User Token doesn't match stored one");
        }

        private string GenerateToken()
        {
            var rand = new Random();
            var tmptoken = string.Empty;

            while (tmptoken.Length <= 24)
                tmptoken += (char)rand.Next(48, 91);

            return tmptoken;
        }
    }
}
