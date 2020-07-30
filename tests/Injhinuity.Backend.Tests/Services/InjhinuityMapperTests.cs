using AutoFixture;
using FluentAssertions;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Xunit;

namespace Injhinuity.Backend.Tests.Services
{
    public class InjhinuityMapperTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly IInjhinuityMapper _subject;

        public InjhinuityMapperTests()
        {
            _subject = new InjhinuityMapper();
        }

        [Fact]
        public void Map_WhenCalled_ThenMapsToTargetType()
        {
            var command = _fixture.Create<Command>();

            var result = _subject.Map<Command, CommandRequest>(command);

            result.Should().NotBeNull();
            result.Should().BeOfType<CommandRequest>();
        }

        [Fact]
        public void MapEnumerable_WhenCalled_ThenMapsToTargetEnumerableType()
        {
            var commands = _fixture.CreateMany<Command>();

            var result = _subject.MapEnumerable<Command, CommandRequest>(commands);

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().AllBeOfType<CommandRequest>();
        }
    }
}
