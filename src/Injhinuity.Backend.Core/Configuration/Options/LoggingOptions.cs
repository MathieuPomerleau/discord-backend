﻿using Microsoft.Extensions.Logging;

namespace Injhinuity.Backend.Core.Configuration.Options
{
    public class LoggingOptions : INullableOption
    {
        private const string OptionName = "Logging";

        public LogLevel? LogLevel { get; set; }

        public void ContainsNull(NullableOptionsResult result)
        {
            if (LogLevel is null)
                result.AddValueToResult(OptionName, "LogLevel");
        }
    }
}
