using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Injhinuity.Backend.Firestore
{
    public interface IFirestoreProvider
    {
        DocumentReference GetGuildReference(string itemId);
        Task<DocumentSnapshot> GetGuildSnapshotAsync(string itemId);
        CollectionReference GetGuildCollectionReference();
        Task<QuerySnapshot> GetGuildCollectionSnapshotAsync();
        DocumentReference GetGuildItemReference(string guildId, string itemId, string itemCollection);
        Task<DocumentSnapshot> GetGuildItemSnapshotAsync(string guildId, string itemId, string itemCollection);
        CollectionReference GetGuildItemCollectionReference(string guildId, string itemCollection);
        Task<QuerySnapshot> GetGuildItemCollectionSnapshotAsync(string guildId, string itemCollection);
    }

    public class FirestoreProvider : IFirestoreProvider
    {
        private readonly FirestoreDb _firestore;

        public FirestoreProvider(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        public DocumentReference GetGuildReference(string itemId) =>
            _firestore.Collection(FirestoreCollections.Guilds).Document(itemId);

        public Task<DocumentSnapshot> GetGuildSnapshotAsync(string itemId) =>
          GetGuildReference(itemId).GetSnapshotAsync();

        public CollectionReference GetGuildCollectionReference() =>
            _firestore.Collection(FirestoreCollections.Guilds);

        public Task<QuerySnapshot> GetGuildCollectionSnapshotAsync() =>
            GetGuildCollectionReference().GetSnapshotAsync();

        public DocumentReference GetGuildItemReference(string guildId, string itemId, string itemCollection) =>
            _firestore
                .Collection(FirestoreCollections.Guilds).Document(guildId)
                .Collection(itemCollection).Document(itemId);

        public Task<DocumentSnapshot> GetGuildItemSnapshotAsync(string guildId, string itemId, string itemCollection) =>
            GetGuildItemReference(guildId, itemId, itemCollection).GetSnapshotAsync();

        public CollectionReference GetGuildItemCollectionReference(string guildId, string itemCollection) =>
            _firestore
                .Collection(FirestoreCollections.Guilds).Document(guildId)
                .Collection(itemCollection);

        public Task<QuerySnapshot> GetGuildItemCollectionSnapshotAsync(string guildId, string itemCollection) =>
            GetGuildItemCollectionReference(guildId, itemCollection).GetSnapshotAsync();
    }
}
