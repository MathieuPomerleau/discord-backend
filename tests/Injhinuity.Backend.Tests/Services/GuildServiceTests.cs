using System;
using System.Collections.Generic;
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
        private readonly IInjhinuityMapper _mapper;

        private readonly string _itemId = _fixture.Create<string>();
        private readonly Guild _guild = _fixture.Create<Guild>();
        private readonly IEnumerable<Guild> _guilds = _fixture.CreateMany<Guild>();
        private readonly GuildEntity _guildEntity = _fixture.Create<GuildEntity>();
        private readonly IEnumerable<GuildEntity> _guildEntities = _fixture.CreateMany<GuildEntity>();

        public GuildServiceTests()
        {
            _guildRepository = Substitute.For<IGuildRepository>();
            _mapper = Substitute.For<IInjhinuityMapper>();
            _mapper.Map<GuildEntity, Guild>(default).ReturnsForAnyArgs(_guild);
            _mapper.MapEnumerable<GuildEntity, Guild>(default).ReturnsForAnyArgs(_guilds);
            _mapper.Map<Guild, GuildEntity>(default).ReturnsForAnyArgs(_guildEntity);
            _mapper.MapEnumerable<Guild, GuildEntity>(default).ReturnsForAnyArgs(_guildEntities);

            _subject = new GuildService(_guildRepository, _mapper);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithANullCommand_ThenThrowException()
        {
            _mapper.Map<Guild, GuildEntity>(default).ReturnsForAnyArgs((GuildEntity)null);

            Func<Task> act = async () => await _subject.CreateAsync(_itemId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage($"Provided Guild is invalid | Id: {_itemId}");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithACommand_ThenCallTheRepository()
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
            _guildRepository.GetByItemIdAsync(default).ReturnsForAnyArgs((GuildEntity)null);
            _mapper.Map<GuildEntity, Guild>(default).ReturnsForAnyArgs((Guild)null);

            Func<Task> act = async () => await _subject.GetByItemIdAsync(_itemId);

            act.Should().Throw<InjhinuityNotFoundWebException>().WithMessage($"Guild was not found | Id: {_itemId}");
        }

        [Fact]
        public async Task GetByItemIdAsync_WhenCalledAndRepositoryReturnsEntities_ThenReturnsEntities()
        {
            _guildRepository.GetByItemIdAsync(default).ReturnsForAnyArgs(_guildEntity);

            var result = await _subject.GetByItemIdAsync(_itemId);

            await _guildRepository.Received().GetByItemIdAsync(_itemId);
            result.Should().BeOfType<Guild>();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledAndRepositoryReturnsEntities_ThenReturnsEntities()
        {
            _guildRepository.GetAllAsync().ReturnsForAnyArgs(_guildEntities);

            var result = await _subject.GetAllAsync();

            await _guildRepository.Received().GetAllAsync();
            result.Should().AllBeOfType<Guild>();
        }

        [Fact]
        public void UpdateAsync_WhenCalled_ThenThrowException()
        {
            _mapper.Map<Guild, GuildEntity>(default).ReturnsForAnyArgs((GuildEntity)null);

            Func<Task> act = async () => await _subject.UpdateAsync(_itemId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage($"Provided Guild is invalid | Id: {_itemId}");
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.UpdateAsync(_itemId, _guild);

            await _guildRepository.Received().UpdateAsync(_itemId, Arg.Any<GuildEntity>());
        }
    }
}
