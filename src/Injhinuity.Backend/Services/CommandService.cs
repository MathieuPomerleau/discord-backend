using System.Collections.Generic;
using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;

namespace Injhinuity.Backend.Services
{
    public interface ICommandService
    {
        Task CreateAsync(string guildId, Command item);
        Task DeleteAsync(string guildId, string itemId);
        Task<Command> GetByItemIdAsync(string guildId, string itemId);
        Task<IEnumerable<Command>?> GetAllAsync(string guildId);
        Task UpdateAsync(string guildId, string itemId, Command item);
    }

    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _repository;
        private readonly IInjhinuityMapper _mapper;

        public CommandService(ICommandRepository repository, IInjhinuityMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task CreateAsync(string guildId, Command item)
        {
            if (_mapper.Map<Command, CommandEntity>(item) is CommandEntity entity)
                return _repository.CreateAsync(guildId, entity.Name, entity);
            else
                throw new InjhinuityBadRequestWebException($"Provided Command is invalid | GuildId: {guildId}, Name: {item?.Name ?? "null"}", "Invalid request format");
        }

        public Task DeleteAsync(string guildId, string itemId) =>
            _repository.DeleteAsync(guildId, itemId);

        public async Task<Command> GetByItemIdAsync(string guildId, string itemId)
        {
            var entity = await _repository.GetByItemIdAsync(guildId, itemId);

            if (_mapper.Map<CommandEntity, Command>(entity) is Command command)
                return command;
            else
                throw new InjhinuityNotFoundWebException($"Command was not found | GuildId: {guildId}, Name: {itemId}", "Resource does not exist");
        }

        public async Task<IEnumerable<Command>?> GetAllAsync(string guildId)
        {
            var entities = await _repository.GetAllAsync(guildId);
            return _mapper.MapEnumerable<CommandEntity, Command>(entities);
        }

        public Task UpdateAsync(string guildId, string itemId, Command item)
        {
            if (_mapper.Map<Command, CommandEntity>(item) is CommandEntity entity)
                return _repository.UpdateAsync(guildId, itemId, entity);
            else
                throw new InjhinuityBadRequestWebException($"Provided Command is invalid | GuildId: {guildId}, Name: {item?.Name ?? "null"}", "Invalid request format");
        }
    }
}
