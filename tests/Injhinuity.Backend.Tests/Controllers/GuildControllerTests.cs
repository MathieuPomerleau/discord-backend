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
    public class GuildControllerTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly GuildController _subject;

        private readonly IGuildService _guildService;
        private readonly IInjhinuityMapper _mapper;

        private readonly string _itemId = _fixture.Create<string>();
        private readonly Guild _guild = _fixture.Create<Guild>();
        private readonly IEnumerable<Guild> _guilds = _fixture.CreateMany<Guild>();
        private readonly GuildRequest _guildRequest = _fixture.Create<GuildRequest>();
        private readonly IEnumerable<GuildRequest> _guildRequests = _fixture.CreateMany<GuildRequest>();

        public GuildControllerTests()
        {
            _guildService = Substitute.For<IGuildService>();
            _mapper = Substitute.For<IInjhinuityMapper>();
            _mapper.Map<GuildRequest, Guild>(default).ReturnsForAnyArgs(_guild);
            _mapper.Map<Guild, GuildRequest>(default).ReturnsForAnyArgs(_guildRequest);
            _mapper.MapEnumerable<GuildRequest, Guild>(default).ReturnsForAnyArgs(_guilds);
            _mapper.MapEnumerable<Guild, GuildRequest>(default).ReturnsForAnyArgs(_guildRequests);

            _subject = new GuildController(_guildService, _mapper);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            _mapper.Map<GuildRequest, Guild>(default).ReturnsForAnyArgs((Guild)null);

            Func<Task> act = async () => await _subject.CreateAsync(null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Guild request was empty or invalid");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.CreateAsync(_guildRequest);

            await _guildService.Received().CreateAsync(_guild.Id, Arg.Any<Guild>());
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ThenCallsItsService()
        {
            await _subject.DeleteAsync(_itemId);

            await _guildService.Received().DeleteAsync(_itemId);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ThenReturnsACommand()
        {
            _guildService.GetByItemIdAsync(default).ReturnsForAnyArgs(_guild);

            var result = await _subject.GetAsync(_itemId) as OkObjectResult;

            await _guildService.Received().GetByItemIdAsync(_itemId);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ThenReturnsAListOfCommands()
        {
            _guildService.GetAllAsync().ReturnsForAnyArgs(_guilds);

            var result = await _subject.GetAllAsync() as OkObjectResult;

            await _guildService.Received().GetAllAsync();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public void UpdateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            _mapper.Map<GuildRequest, Guild>(default).ReturnsForAnyArgs((Guild)null);

            Func<Task> act = async () => await _subject.UpdateAsync(null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Guild request was empty or invalid");
        }

        [Fact]
        public async Task UpdateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.UpdateAsync(_guildRequest);

            await _guildService.Received().UpdateAsync(_guild.Id, Arg.Any<Guild>());
        }
    }
}
