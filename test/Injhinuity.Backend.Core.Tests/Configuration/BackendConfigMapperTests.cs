using System;
using FluentAssertions;
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
        public void MapFromNullableOptions_WhenCalledWithValidOptions_ThenMapsToAConfigObject()
        {
            var options = CreateValidOptions();

            var result = _subject.MapFromNullableOptions(options);

            result.Should().BeOfType<BackendConfig>();
            result.Firestore.Should().NotBeNull();
            result.Firestore.ProjectId.Should().NotBeNull();
            result.Firestore.IsEmulated.Should().BeTrue();
            result.Version.Should().NotBeNull();
            result.Version.VersionNo.Should().NotBeNull();
            result.Logging.Should().NotBeNull();
            result.Logging.LogLevel.Should().NotBeNull();
        }

        [Fact]
        public void MapFromNullableOptions_WhenCalledWithInvalidOptions_ThenThrowAnInjhinuityException()
        {
            var options = CreateValidOptions();
            options.Firestore = null;
            options.Version = null;
            options.Logging = null;

            Action action = () => _subject.MapFromNullableOptions(options);

            action.Should().Throw<InjhinuityException>().WithMessage("Config validation failed, missing value found");
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
