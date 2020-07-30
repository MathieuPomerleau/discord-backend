#nullable disable
using Injhinuity.Backend.Core.Configuration.Options;
using Injhinuity.Backend.Core.Exceptions;

namespace Injhinuity.Backend.Core.Configuration
{
    public interface IBackendConfigMapper
    {
        IBackendConfig MapFromNullableOptions(IBackendOptions options);
    }

    public class BackendConfigMapper : IBackendConfigMapper
    {
        public IBackendConfig MapFromNullableOptions(IBackendOptions backendOptions)
        {
            if (!(backendOptions is BackendOptions options))
                throw new InjhinuityException("Configuration couldn't be built, options are null");

            var result = new NullableOptionsResult();
            options.ContainsNull(result);

            if (!result.IsValid)
                throw new InjhinuityException($"The following values are missing from the configuration:\n{result}");

            var version = new VersionConfig(options.Version.VersionNo);
            var logging = new LoggingConfig(options.Logging.LogLevel.Value);
            var firestore = new FirestoreConfig(options.Firestore.ProjectId, options.Firestore.IsEmulated.Value);

            return new BackendConfig(version, logging, firestore);
        }
    }
}
