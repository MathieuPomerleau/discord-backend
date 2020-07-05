namespace Injhinuity.Backend.Core.Configuration.Options
{
    public interface IBackendOptions : INullableOption
    {
        static string SectionName => "Backend";

        VersionOptions? Version { get; set; }
        LoggingOptions? Logging { get; set; }
        FirestoreOptions? Firestore { get; set; }
    }

    public class BackendOptions : IBackendOptions
    {
        public VersionOptions? Version { get; set; }
        public LoggingOptions? Logging { get; set; }
        public FirestoreOptions? Firestore { get; set; }

        public bool ContainsNull() =>
            (Version?.ContainsNull() ?? true) ||
            (Logging?.ContainsNull() ?? true) ||
            (Firestore?.ContainsNull() ?? true);
    }
}
