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
        public void ContainsNull_WhenCalledWithNonNullOptions_ThenReturnsFalse()
        {
            var result = _subject.ContainsNull();

            result.Should().BeFalse();
        }

        [Fact]
        public void ContainsNull_WhenCalledWithAtLeastOneNullOptions_ThenReturnsTrue()
        {
            _subject.ProjectId = null;
            _subject.IsEmulated = null;

            var result = _subject.ContainsNull();

            result.Should().BeTrue();
        }
    }
}
