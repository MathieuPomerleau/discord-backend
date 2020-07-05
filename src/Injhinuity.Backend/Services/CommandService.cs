using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;
using Mapster;

namespace Injhinuity.Backend.Services
{
    public interface ICommandService
    {
        Task CreateAsync(string guildId, Command item);
        Task DeleteAsync(string guildId, string itemId);
        Task<Command> GetByItemIdAsync(string guildId, string itemId);
        Task UpdateAsync(string guildId, string itemId, Command item);
    }

    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _repository;

        public CommandService(ICommandRepository repository)
        {
            _repository = repository;
        }

        public Task CreateAsync(string guildId, Command item) =>
            _repository.CreateAsync(guildId, item.Name, item.Adapt<CommandEntity>());

        public Task DeleteAsync(string guildId, string itemId) =>
            _repository.DeleteAsync(guildId, itemId);

        public async Task<Command> GetByItemIdAsync(string guildId, string itemId)
        {
            var entity = await _repository.GetByItemIdAsync(guildId, itemId);

            if (entity?.Adapt<Command>() is Command command)
                return command;
            else
                throw new InjhinuityNotFoundWebException($"Command was not found | GuildId: {guildId}, Name: {itemId}");
        }

        public Task UpdateAsync(string guildId, string itemId, Command item) =>
            _repository.UpdateAsync(guildId, itemId, item.Adapt<CommandEntity>());
    }
}
