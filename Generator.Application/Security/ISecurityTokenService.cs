using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Generator.Application.Security
{
    public interface ISecurityTokenService
    {
        Task<string> SaveDataWithTokenAsync(IEnumerable<string> list, Guid userId);

        Task<IList<string>> GetSavedData(Guid userId, string token);
    }
}
