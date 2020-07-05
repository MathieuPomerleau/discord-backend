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
        public void ContainsNull_WhenCalledWithNonNullOptions_ThenReturnsFalse()
        {
            var result = _subject.ContainsNull();

            result.Should().BeFalse();
        }

        [Fact]
        public void ContainsNull_WhenCalledWithAtLeastOneNullOptions_ThenReturnsTrue()
        {
            _subject.Firestore = null;

            var result = _subject.ContainsNull();

            result.Should().BeTrue();
        }
    }
}
