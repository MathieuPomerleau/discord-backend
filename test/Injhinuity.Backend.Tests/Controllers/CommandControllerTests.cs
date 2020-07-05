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
    public class CommandControllerTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly CommandController _subject;
        private readonly ICommandService _commandService;

        private readonly string _guildId = _fixture.Create<string>();
        private readonly string _itemId = _fixture.Create<string>();
        private readonly CommandRequest _commandRequest = _fixture.Create<CommandRequest>();

        public CommandControllerTests()
        {
            _commandService = Substitute.For<ICommandService>();
            _subject = new CommandController(_commandService);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            Func<Task> act = async () => await _subject.CreateAsync(_guildId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Command request was empty or invalid");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.CreateAsync(_guildId, _commandRequest);

            await _commandService.Received().CreateAsync(_guildId, Arg.Any<Command>());
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ThenReturnsCommand()
        {
            await _subject.GetAsync(_guildId, _itemId);

            await _commandService.Received().GetByItemIdAsync(_guildId, _itemId);
        }
    }
}
