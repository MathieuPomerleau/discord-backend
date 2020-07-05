using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;
using Mapster;

namespace Injhinuity.Backend.Services
{
    public interface IGuildService
    {
        Task CreateAsync(string itemId, Guild item);

        Task DeleteAsync(string itemId);

        Task<Guild> GetByItemIdAsync(string itemId);

        Task UpdateAsync(string itemId, Guild item);
    }

    public class GuildService : IGuildService
    {
        private readonly IGuildRepository _repository;

        public GuildService(IGuildRepository repository)
        {
            _repository = repository;
        }

        public Task CreateAsync(string itemId, Guild item) =>
            _repository.CreateAsync(itemId, item.Adapt<GuildEntity>());

        public Task DeleteAsync(string itemId) =>
            _repository.DeleteAsync(itemId);

        public async Task<Guild> GetByItemIdAsync(string itemId)
        {
            var entity = await _repository.GetByItemIdAsync(itemId);

            if (entity?.Adapt<Guild>() is Guild guild)
                return guild;
            else
                throw new InjhinuityNotFoundWebException($"Guild was not found | Id: {itemId}");
        }

        public Task UpdateAsync(string itemId, Guild item) =>
            _repository.UpdateAsync(itemId, item.Adapt<GuildEntity>());
    }
}
