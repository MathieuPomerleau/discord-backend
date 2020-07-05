using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Injhinuity.Backend.Controllers;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using NSubstitute;
using Xunit;

namespace Injhinuity.Backend.Tests.Controllers
{
    public class GuildControllerTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly GuildController _subject;
        private readonly IGuildService _guildService;

        private readonly string _itemId = _fixture.Create<string>();
        private readonly GuildRequest _guildRequest = _fixture.Create<GuildRequest>();

        public GuildControllerTests()
        {
            _guildService = Substitute.For<IGuildService>();
            _subject = new GuildController(_guildService);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            Func<Task> act = async () => await _subject.CreateAsync(null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Guild request was empty or invalid");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.CreateAsync(_guildRequest);

            await _guildService.ReceivedWithAnyArgs().CreateAsync(_itemId, Arg.Any<Guild>());
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ThenReturnsCommand()
        {
            await _subject.GetAsync(_itemId);

            await _guildService.ReceivedWithAnyArgs().GetByItemIdAsync(_itemId);
        }
    }
}
