using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Generator.Application.Security
{
    public interface ISecurityToken
    {
        Task<string> SaveDataWithTokenAsync(IEnumerable<string> list, Guid userId);

        IList<string> GetSavedData(Guid userId, string token);
    }
}
