using System.Collections.Generic;
using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;

namespace Injhinuity.Backend.Services
{
    public interface IGuildService
    {
        Task CreateAsync(string itemId, Guild item);
        Task DeleteAsync(string itemId);
        Task<Guild> GetByItemIdAsync(string itemId);
        Task<IEnumerable<Guild>?> GetAllAsync();
        Task UpdateAsync(string itemId, Guild item);
    }

    public class GuildService : IGuildService
    {
        private readonly IGuildRepository _repository;
        private readonly IInjhinuityMapper _mapper;

        public GuildService(IGuildRepository repository, IInjhinuityMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task CreateAsync(string itemId, Guild item)
        {
            if (_mapper.Map<Guild, GuildEntity>(item) is GuildEntity entity)
                return _repository.CreateAsync(itemId, entity);
            else
                throw new InjhinuityBadRequestWebException($"Provided Guild is invalid | Id: {itemId}", "Invalid request format");
        }

        public Task DeleteAsync(string itemId) =>
            _repository.DeleteAsync(itemId);

        public async Task<Guild> GetByItemIdAsync(string itemId)
        {
            var entity = await _repository.GetByItemIdAsync(itemId);

            if (_mapper.Map<GuildEntity, Guild>(entity) is Guild guild)
                return guild;
            else
                throw new InjhinuityNotFoundWebException($"Guild was not found | Id: {itemId}", "Resource does not exist");
        }

        public async Task<IEnumerable<Guild>?> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.MapEnumerable<GuildEntity, Guild>(entities);
        }

        public Task UpdateAsync(string itemId, Guild item)
        {
            if (_mapper.Map<Guild, GuildEntity>(item) is GuildEntity entity)
                return _repository.UpdateAsync(itemId, entity);
            else
                throw new InjhinuityBadRequestWebException($"Provided Guild is invalid | Id: {itemId}", "Invalid request format");
        }
    }
}
