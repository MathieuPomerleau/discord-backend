using System.Collections.Generic;
using System.Threading.Tasks;
using Injhinuity.Backend.Model.Entities;

namespace Injhinuity.Backend.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task CreateAsync(string guildId, string itemId, RoleEntity entity);
        Task DeleteAsync(string guildId, string itemId);
        Task<RoleEntity?> GetByItemIdAsync(string guildId, string itemId);
        Task<IEnumerable<RoleEntity>?> GetAllAsync(string guildId);
        Task UpdateAsync(string guildId, string itemId, RoleEntity entity);
    }
}
