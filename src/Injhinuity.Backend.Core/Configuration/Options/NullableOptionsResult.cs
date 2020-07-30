using System.Collections.Generic;
using System.Text;

namespace Injhinuity.Backend.Core.Configuration.Options
{
    public class NullableOptionsResult
    {
        public IList<(string, string)> NullValues { get; } = new List<(string, string)>();
        public bool IsValid => NullValues.Count == 0;

        public NullableOptionsResult AddValueToResult(string optionName, string valueName)
        {
            NullValues.Add((optionName, valueName));
            return this;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var (name, value) in NullValues)
                builder.Append($"\n{name} - {value}");

            return builder.ToString();
        }
    }
}
