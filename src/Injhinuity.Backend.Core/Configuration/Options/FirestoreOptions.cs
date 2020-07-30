namespace Injhinuity.Backend.Core.Configuration.Options
{
    public class FirestoreOptions : INullableOption
    {
        private const string OptionName = "Firestore";

        public string? ProjectId { get; set; }
        public bool? IsEmulated { get; set; }

        public void ContainsNull(NullableOptionsResult result)
        {
            if (ProjectId is null)
                result.AddValueToResult(OptionName, "ProjectId");

            if (IsEmulated is null)
                result.AddValueToResult(OptionName, "IsEmulated");
        }
    }
}
