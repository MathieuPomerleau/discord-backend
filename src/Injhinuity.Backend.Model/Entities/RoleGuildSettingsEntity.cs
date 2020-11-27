#nullable disable
using Google.Cloud.Firestore;

namespace Injhinuity.Backend.Model.Entities
{
    [FirestoreData]
    public class RoleGuildSettingsEntity
    {
        [FirestoreProperty]
        public string ReactionRoleChannelId { get; set; }
        [FirestoreProperty]
        public string ReactionRoleMessageId { get; set; }
        [FirestoreProperty]
        public string MuteRoleId { get; set; }
    }
}
