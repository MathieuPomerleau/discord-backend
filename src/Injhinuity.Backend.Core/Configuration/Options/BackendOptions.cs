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
        private const string OptionName = "Backend";

        public VersionOptions? Version { get; set; }
        public LoggingOptions? Logging { get; set; }
        public FirestoreOptions? Firestore { get; set; }

        public void ContainsNull(NullableOptionsResult result)
        {
            if (Version is null)
                result.AddValueToResult(OptionName, "Version");
            else
                Version.ContainsNull(result);

            if (Logging is null)
                result.AddValueToResult(OptionName, "Logging");
            else
                Logging.ContainsNull(result);

            if (Firestore is null)
                result.AddValueToResult(OptionName, "Firestore");
            else
                Firestore.ContainsNull(result);
        }
    }
}
