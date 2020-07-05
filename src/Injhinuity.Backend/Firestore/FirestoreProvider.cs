using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Injhinuity.Backend.Firestore
{
    public interface IFirestoreProvider
    {
        DocumentReference GetGuildReference(string itemId);
        Task<DocumentSnapshot> GetGuildSnapshot(string itemId);
        DocumentReference GetGuildItemReference(string guildId, string itemId, string itemCollection);
        Task<DocumentSnapshot> GetGuildItemSnapshot(string guildId, string itemId, string itemCollection);
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

        public Task<DocumentSnapshot> GetGuildSnapshot(string itemId) =>
            GetGuildReference(itemId).GetSnapshotAsync();

        public DocumentReference GetGuildItemReference(string guildId, string itemId, string itemCollection) =>
            _firestore
                .Collection(FirestoreCollections.Guilds).Document(guildId)
                .Collection(itemCollection).Document(itemId);

        public Task<DocumentSnapshot> GetGuildItemSnapshot(string guildId, string itemId, string itemCollection) =>
            GetGuildItemReference(guildId, itemId, itemCollection).GetSnapshotAsync();
    }
}
