namespace Injhinuity.Backend.Core.Configuration.Options
{
    public class FirestoreOptions : INullableOption
    {
        public string? ProjectId { get; set; }
        public bool? IsEmulated { get; set; }

        public bool ContainsNull() =>
            ProjectId is null ||
            IsEmulated is null;
    }
}
