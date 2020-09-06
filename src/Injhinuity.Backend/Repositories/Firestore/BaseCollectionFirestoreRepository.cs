using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Injhinuity.Backend.Firestore;

namespace Injhinuity.Backend.Repositories.Firestore
{
    public abstract class BaseCollectionFirestoreRepository
    {
        private readonly IFirestoreProvider _firestoreProvider;
        private readonly string _collection;

        protected BaseCollectionFirestoreRepository(IFirestoreProvider firestoreProvider, string collection)
        {
            _firestoreProvider = firestoreProvider;
            _collection = collection;
        }

        protected DocumentReference ItemReference(string guildId, string itemId) =>
            _firestoreProvider.GetGuildItemReference(guildId, itemId, _collection);

        protected Task<DocumentSnapshot> ItemSnapshotAsync(string guildId, string itemId) =>
            _firestoreProvider.GetGuildItemSnapshotAsync(guildId, itemId, _collection);

        protected Task<QuerySnapshot> CollectionSnapshotAsync(string guildId) =>
            _firestoreProvider.GetGuildItemCollectionSnapshotAsync(guildId, _collection);
    }
}
