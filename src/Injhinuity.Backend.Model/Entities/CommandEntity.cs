using Google.Cloud.Firestore;

namespace Injhinuity.Backend.Model.Entities
{
    [FirestoreData]
    public class CommandEntity
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Body { get; set; }
    }
}
