using System.Collections.Generic;
using System.Threading.Tasks;
using Injhinuity.Backend.Model.Entities;

namespace Injhinuity.Backend.Repositories.Interfaces
{
    public interface ICommandRepository
    {
        Task CreateAsync(string guildId, string itemId, CommandEntity entity);
        Task DeleteAsync(string guildId, string itemId);
        Task<CommandEntity?> GetByItemIdAsync(string guildId, string itemId);
        Task<IEnumerable<CommandEntity>?> GetAllAsync(string guildId);
        Task UpdateAsync(string guildId, string itemId, CommandEntity entity);
    }
}
