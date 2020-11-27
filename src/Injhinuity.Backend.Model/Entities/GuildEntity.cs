#nullable disable
using Google.Cloud.Firestore;

namespace Injhinuity.Backend.Model.Entities
{
    [FirestoreData]
    public class GuildEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public RoleGuildSettingsEntity RoleSettings { get; set; }
    }
}
