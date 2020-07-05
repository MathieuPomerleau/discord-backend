namespace Injhinuity.Backend.Core.Configuration
{
    public class FirestoreConfig
    {
        public string ProjectId { get; set; }
        public bool IsEmulated { get; set; }

        public FirestoreConfig(string projectId, bool isEmulated)
        {
            ProjectId = projectId;
            IsEmulated = isEmulated;
        }
    }
}
