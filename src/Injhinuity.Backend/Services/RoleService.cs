using System.Collections.Generic;
using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;

namespace Injhinuity.Backend.Services
{
    public interface IRoleService
    {
        Task CreateAsync(string guildId, Role item);
        Task DeleteAsync(string guildId, string itemId);
        Task<Role> GetByItemIdAsync(string guildId, string itemId);
        Task<IEnumerable<Role>?> GetAllAsync(string guildId);
        Task UpdateAsync(string guildId, string itemId, Role item);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IInjhinuityMapper _mapper;

        public RoleService(IRoleRepository repository, IInjhinuityMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task CreateAsync(string guildId, Role item)
        {
            if (_mapper.Map<Role, RoleEntity>(item) is RoleEntity entity)
                return _repository.CreateAsync(guildId, entity.Name, entity);
            else
                throw new InjhinuityBadRequestWebException($"Provided Role is invalid | GuildId: {guildId}, Name: {item?.Name ?? "null"}", "Invalid request format");
        }

        public Task DeleteAsync(string guildId, string itemId) =>
            _repository.DeleteAsync(guildId, itemId);

        public async Task<Role> GetByItemIdAsync(string guildId, string itemId)
        {
            var entity = await _repository.GetByItemIdAsync(guildId, itemId);

            if (_mapper.Map<RoleEntity, Role>(entity) is Role command)
                return command;
            else
                throw new InjhinuityNotFoundWebException($"Role was not found | GuildId: {guildId}, Name: {itemId}", "Resource does not exist");
        }

        public async Task<IEnumerable<Role>?> GetAllAsync(string guildId)
        {
            var entities = await _repository.GetAllAsync(guildId);
            return _mapper.MapEnumerable<RoleEntity, Role>(entities);
        }

        public Task UpdateAsync(string guildId, string itemId, Role item)
        {
            if (_mapper.Map<Role, RoleEntity>(item) is RoleEntity entity)
                return _repository.UpdateAsync(guildId, itemId, entity);
            else
                throw new InjhinuityBadRequestWebException($"Provided Role is invalid | GuildId: {guildId}, Name: {item?.Name ?? "null"}", "Invalid request format");
        }
    }
}
