﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Injhinuity.Backend.Firestore;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;

namespace Injhinuity.Backend.Repositories.Firestore
{
    public class CommandFirestoreRepository : BaseCollectionFirestoreRepository, ICommandRepository
    {
        public CommandFirestoreRepository(IFirestoreProvider firestoreProvider) : base(firestoreProvider, FirestoreCollections.Commands)
        { 
        }

        public async Task CreateAsync(string guildId, string itemId, CommandEntity entity)
        {
            var reference = ItemReference(guildId, itemId);
            await reference.CreateAsync(entity);
        }

        public async Task DeleteAsync(string guildId, string itemId)
        {
            var reference = ItemReference(guildId, itemId);
            await reference.DeleteAsync();
        }

        public async Task<CommandEntity?> GetByItemIdAsync(string guildId, string itemId)
        {
            var snapshot = await ItemSnapshotAsync(guildId, itemId);
            return snapshot.ConvertTo<CommandEntity>();
        }

        public async Task<IEnumerable<CommandEntity>?> GetAllAsync(string guildId)
        {
            var snapshot = await CollectionSnapshotAsync(guildId);
            return snapshot.Documents.Select(x => x.ConvertTo<CommandEntity>());
        }

        public async Task UpdateAsync(string guildId, string itemId, CommandEntity entity)
        {
            var reference = ItemReference(guildId, itemId);
            await reference.SetAsync(entity, SetOptions.MergeAll);
        }
    }
}
