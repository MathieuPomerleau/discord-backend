﻿using FluentAssertions;
using Injhinuity.Backend.Core.Configuration.Options;
using Xunit;

namespace Injhinuity.Backend.Core.Tests.Configuration.Options
{
    public class VersionOptionsTests
    {
        private readonly VersionOptions _subject;

        public VersionOptionsTests()
        {
            _subject = new VersionOptions { VersionNo = "0" };
        }

        [Fact]
        public void ContainsNull_WhenCalledWithNonNullProperties_ThenResultIsValid()
        {
            var result = new NullableOptionsResult();

            _subject.ContainsNull(result);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ContainsNull_WhenCalledWithAtLeastOneNullOptions_ThenResultIsNotValid()
        {
            var result = new NullableOptionsResult();
            _subject.VersionNo = null;

            _subject.ContainsNull(result);

            result.IsValid.Should().BeFalse();
            result.NullValues.Should().Contain(("Version", "VersionNo"));
        }
    }
}
