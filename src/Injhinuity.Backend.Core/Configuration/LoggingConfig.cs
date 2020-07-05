using Microsoft.Extensions.Logging;

namespace Injhinuity.Backend.Core.Configuration
{
    public class LoggingConfig
    {
        public LogLevel LogLevel { get; }

        public LoggingConfig(LogLevel logLevel)
        {
            LogLevel = logLevel;
        }
    }
}
