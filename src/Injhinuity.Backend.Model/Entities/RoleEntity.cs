using Google.Cloud.Firestore;

namespace Injhinuity.Backend.Model.Entities
{
    [FirestoreData]
    public class RoleEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
    }
}
