using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Injhinuity.Backend.Firestore;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;

namespace Injhinuity.Backend.Repositories.Firestore
{
    public class RoleFirestoreRepository : BaseCollectionFirestoreRepository, IRoleRepository
    {
        public RoleFirestoreRepository(IFirestoreProvider firestoreProvider) : base(firestoreProvider, FirestoreCollections.Roles)
        {
        }

        public async Task CreateAsync(string guildId, string itemId, RoleEntity entity)
        {
            var reference = ItemReference(guildId, itemId);
            await reference.CreateAsync(entity);
        }

        public async Task DeleteAsync(string guildId, string itemId)
        {
            var reference = ItemReference(guildId, itemId);
            await reference.DeleteAsync();
        }

        public async Task<RoleEntity?> GetByItemIdAsync(string guildId, string itemId)
        {
            var snapshot = await ItemSnapshotAsync(guildId, itemId);
            return snapshot.ConvertTo<RoleEntity>();
        }

        public async Task<IEnumerable<RoleEntity>?> GetAllAsync(string guildId)
        {
            var snapshot = await CollectionSnapshotAsync(guildId);
            return snapshot.Documents.Select(x => x.ConvertTo<RoleEntity>());
        }

        public async Task UpdateAsync(string guildId, string itemId, RoleEntity entity)
        {
            var reference = ItemReference(guildId, itemId);
            await reference.SetAsync(entity, SetOptions.MergeAll);
        }
    }
}
