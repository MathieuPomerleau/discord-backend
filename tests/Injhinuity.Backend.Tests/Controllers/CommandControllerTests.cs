using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Injhinuity.Backend.Controllers;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Injhinuity.Backend.Tests.Controllers
{
    public class CommandControllerTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly CommandController _subject;

        private readonly ICommandService _commandService;
        private readonly IInjhinuityMapper _mapper;

        private readonly string _guildId = _fixture.Create<string>();
        private readonly string _itemId = _fixture.Create<string>();
        private readonly Command _command = _fixture.Create<Command>();
        private readonly IEnumerable<Command> _commands = _fixture.CreateMany<Command>();
        private readonly CommandRequest _commandRequest = _fixture.Create<CommandRequest>();
        private readonly IEnumerable<CommandRequest> _commandRequests = _fixture.CreateMany<CommandRequest>();

        public CommandControllerTests()
        {
            _commandService = Substitute.For<ICommandService>();
            _mapper = Substitute.For<IInjhinuityMapper>();
            _mapper.Map<CommandRequest, Command>(default).ReturnsForAnyArgs(_command);
            _mapper.Map<Command, CommandRequest>(default).ReturnsForAnyArgs(_commandRequest);
            _mapper.MapEnumerable<CommandRequest, Command>(default).ReturnsForAnyArgs(_commands);
            _mapper.MapEnumerable<Command, CommandRequest>(default).ReturnsForAnyArgs(_commandRequests);

            _subject = new CommandController(_commandService, _mapper);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            _mapper.Map<CommandRequest, Command>(default).ReturnsForAnyArgs((Command)null);

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
        public async Task DeleteAsync_WhenCalled_ThenCallsItsService()
        {
            await _subject.DeleteAsync(_guildId, _itemId);

            await _commandService.Received().DeleteAsync(_guildId, _itemId);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ThenReturnsACommand()
        {
            _commandService.GetByItemIdAsync(default, default).ReturnsForAnyArgs(_command);

            var result = await _subject.GetAsync(_guildId, _itemId) as OkObjectResult;

            await _commandService.Received().GetByItemIdAsync(_guildId, _itemId);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ThenReturnsAListOfCommands()
        {
            _commandService.GetAllAsync(default).ReturnsForAnyArgs(_commands);

            var result = await _subject.GetAllAsync(_guildId) as OkObjectResult;

            await _commandService.Received().GetAllAsync(_guildId);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public void UpdateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            _mapper.Map<CommandRequest, Command>(default).ReturnsForAnyArgs((Command)null);

            Func<Task> act = async () => await _subject.UpdateAsync(_guildId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Command request was empty or invalid");
        }

        [Fact]
        public async Task UpdateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.UpdateAsync(_guildId, _commandRequest);

            await _commandService.Received().UpdateAsync(_guildId, _command.Name, Arg.Any<Command>());
        }
    }
}
