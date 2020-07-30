using FluentAssertions;
using Injhinuity.Backend.Core.Configuration.Options;
using Xunit;

namespace Injhinuity.Backend.Core.Tests.Configuration.Options
{
    public class FirestoreOptionsTests
    {
        private readonly FirestoreOptions _subject;

        public FirestoreOptionsTests()
        {
            _subject = new FirestoreOptions { ProjectId = "projectId", IsEmulated = true };
        }

        [Fact]
        public void ContainsNull_WhenCalledWithNonNullOptions_ThenResultIsValid()
        {
            var result = new NullableOptionsResult();

            _subject.ContainsNull(result);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ContainsNull_WhenCalledWithAtLeastOneNullOptions_ThenResultIsNotValid()
        {
            var result = new NullableOptionsResult();
            _subject.ProjectId = null;
            _subject.IsEmulated = null;

            _subject.ContainsNull(result);

            result.IsValid.Should().BeFalse();
            result.NullValues.Should().Contain(("Firestore", "ProjectId"));
            result.NullValues.Should().Contain(("Firestore", "IsEmulated"));
        }
    }
}
