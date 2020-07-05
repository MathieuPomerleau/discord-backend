namespace Injhinuity.Backend.Core.Configuration
{
    public interface IBackendConfig
    {
        VersionConfig Version { get; }
        LoggingConfig Logging { get; }
        FirestoreConfig Firestore { get; }
    }

    public class BackendConfig : IBackendConfig
    {
        public VersionConfig Version { get; }
        public LoggingConfig Logging { get; }
        public FirestoreConfig Firestore { get; }

        public BackendConfig(VersionConfig version, LoggingConfig logging, FirestoreConfig firestore)
        {
            Version = version;
            Logging = logging;
            Firestore = firestore;
        }
    }
}
