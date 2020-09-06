using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Injhinuity.Backend.Firestore;

namespace Injhinuity.Backend.Repositories.Firestore
{
    public abstract class BaseFirestoreRepository
    {
        private readonly IFirestoreProvider _firestoreProvider;

        protected BaseFirestoreRepository(IFirestoreProvider firestoreProvider)
        {
            _firestoreProvider = firestoreProvider;
        }

        protected DocumentReference Reference(string itemId) =>
            _firestoreProvider.GetGuildReference(itemId);

        protected Task<DocumentSnapshot> SnapshotAsync(string itemId) =>
            _firestoreProvider.GetGuildSnapshotAsync(itemId);

        protected Task<QuerySnapshot> CollectionSnapshotAsync() =>
            _firestoreProvider.GetGuildCollectionSnapshotAsync();
    }
}
