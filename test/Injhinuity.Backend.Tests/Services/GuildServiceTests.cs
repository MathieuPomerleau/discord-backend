using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Entities;
using Injhinuity.Backend.Repositories.Interfaces;
using Injhinuity.Backend.Services;
using NSubstitute;
using Xunit;

namespace Injhinuity.Backend.Tests.Services
{
    public class GuildServiceTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly IGuildService _subject;
        private readonly IGuildRepository _guildRepository;

        private readonly string _itemId = _fixture.Create<string>();
        private readonly Guild _guild = _fixture.Create<Guild>();
        private readonly GuildEntity _guildEntity = _fixture.Create<GuildEntity>();

        public GuildServiceTests()
        {
            _guildRepository = Substitute.For<IGuildRepository>();
            _subject = new GuildService(_guildRepository);
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.CreateAsync(_itemId, _guild);

            await _guildRepository.Received().CreateAsync(_itemId, Arg.Any<GuildEntity>());
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.DeleteAsync(_itemId);

            await _guildRepository.Received().DeleteAsync(_itemId);
        }

        [Fact]
        public void GetByItemIdAsync_WhenCalledAndRepositoryReturnsNull_ThenThrowException()
        {
            GuildEntity entity = null;
            _guildRepository.GetByItemIdAsync(default).ReturnsForAnyArgs(entity);

            Func<Task> act = async () => await _subject.GetByItemIdAsync(_itemId);

            act.Should().Throw<InjhinuityNotFoundWebException>().WithMessage($"Guild was not found | Id: {_itemId}");
        }

        [Fact]
        public async Task GetByItemIdAsync_WhenCalledAndRepositoryReturnsEntity_ThenReturnsEntity()
        {
            _guildRepository.GetByItemIdAsync(default).ReturnsForAnyArgs(_guildEntity);

            var result = await _subject.GetByItemIdAsync(_itemId);

            await _guildRepository.ReceivedWithAnyArgs().GetByItemIdAsync(_itemId);
            result.Should().BeOfType<Guild>();
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.UpdateAsync(_itemId, _guild);

            await _guildRepository.ReceivedWithAnyArgs().UpdateAsync(_itemId, Arg.Any<GuildEntity>());
        }
    }
}
