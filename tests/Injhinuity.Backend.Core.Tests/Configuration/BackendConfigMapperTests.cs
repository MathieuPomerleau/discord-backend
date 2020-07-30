using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Injhinuity.Backend.Core.Configuration;
using Injhinuity.Backend.Core.Configuration.Options;
using Injhinuity.Backend.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Injhinuity.Backend.Core.Tests.Configuration
{
    public class BackendConfigMapperTests
    {
        private readonly IBackendConfigMapper _subject;

        public BackendConfigMapperTests()
        {
            _subject = new BackendConfigMapper();
        }

        [Fact]
        public void MapFromNullableOptions_WhenCalledWithNullClientOptions_ThenThrowAnInjhinuityException()
        {
            Action action = () => _subject.MapFromNullableOptions(null);

            action.Should().Throw<InjhinuityException>().WithMessage("Configuration couldn't be built, options are null");
        }

        [Fact]
        public void MapFromNullableOptions_WhenCalledWithInvalidOptions_ThenThrowAnInjhinuityException()
        {
            var options = CreateValidOptions();
            options.Firestore = null;
            options.Version = null;
            options.Logging = null;

            Action action = () => _subject.MapFromNullableOptions(options);

            action.Should().Throw<InjhinuityException>().WithMessage("The following values are missing from the configuration:\nBackend - Version\nBackend - Logging\nBackend - Firestore");
        }

        [Fact]
        public void MapFromNullableOptions_WhenCalledWithValidOptions_ThenMapsToAConfigObject()
        {
            var options = CreateValidOptions();

            var result = _subject.MapFromNullableOptions(options);

            using var scope = new AssertionScope();

            result.Should().BeOfType<BackendConfig>();
            result.Firestore.Should().NotBeNull();
            result.Firestore.ProjectId.Should().NotBeNull();
            result.Firestore.IsEmulated.Should().BeTrue();
            result.Version.Should().NotBeNull();
            result.Version.VersionNo.Should().NotBeNull();
            result.Logging.Should().NotBeNull();
            result.Logging.LogLevel.Should().NotBeNull();
        }

        private IBackendOptions CreateValidOptions() =>
            new BackendOptions
            {
                Firestore = new FirestoreOptions { ProjectId = "projectId", IsEmulated = true },
                Logging = new LoggingOptions { LogLevel = LogLevel.Information },
                Version = new VersionOptions { VersionNo = "0" }
            };
    }
}
