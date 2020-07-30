using FluentAssertions;
using Injhinuity.Backend.Core.Configuration.Options;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Injhinuity.Backend.Core.Tests.Configuration.Options
{
    public class BackendOptionsTests
    {
        private readonly IBackendOptions _subject;

        public BackendOptionsTests()
        {
            _subject = new BackendOptions
            {
                Firestore = new FirestoreOptions { ProjectId = "projectId", IsEmulated = true },
                Logging = new LoggingOptions { LogLevel = LogLevel.Information },
                Version = new VersionOptions { VersionNo = "0" }
            };
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
            _subject.Firestore = null;

            _subject.ContainsNull(result);

            result.IsValid.Should().BeFalse();
            result.NullValues.Should().Contain(("Backend", "Firestore"));
        }
    }
}
