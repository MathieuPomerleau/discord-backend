using System.Collections.Generic;
using System.Threading.Tasks;
using Injhinuity.Backend.Model.Entities;

namespace Injhinuity.Backend.Repositories.Interfaces
{
    public interface IGuildRepository
    {
        Task CreateAsync(string itemId, GuildEntity entity);
        Task DeleteAsync(string itemId);
        Task<GuildEntity?> GetByItemIdAsync(string itemId);
        Task<IEnumerable<GuildEntity>?> GetAllAsync();
        Task UpdateAsync(string itemId, GuildEntity entity);
    }
}
