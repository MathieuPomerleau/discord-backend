using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Injhinuity.Backend.Firestore;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;

namespace Injhinuity.Backend.Repositories.Firestore
{
    public class GuildFirestoreRepository : BaseFirestoreRepository, IGuildRepository
    {
        public GuildFirestoreRepository(IFirestoreProvider firestoreProvider) : base(firestoreProvider)
        {
        }

        public async Task CreateAsync(string itemId, GuildEntity entity)
        {
            var reference = Reference(itemId);
            await reference.CreateAsync(entity);
        }

        public async Task DeleteAsync(string itemId)
        {
            var reference = Reference(itemId);
            await reference.DeleteAsync();
        }

        public async Task<GuildEntity?> GetByItemIdAsync(string itemId)
        {
            var snapshot = await SnapshotAsync(itemId);
            return snapshot.ConvertTo<GuildEntity>();
        }

        public async Task<IEnumerable<GuildEntity>?> GetAllAsync()
        {
            var snapshot = await CollectionSnapshotAsync();
            return snapshot.Documents.Select(x => x.ConvertTo<GuildEntity>());
        }

        public async Task UpdateAsync(string itemId, GuildEntity entity)
        {
            var reference = Reference(itemId);
            await reference.SetAsync(entity, SetOptions.MergeAll);
        }
    }
}
