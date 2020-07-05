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
    public class CommandServiceTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly ICommandService _subject;
        private readonly ICommandRepository _commandRepository;

        private readonly string _guildId = _fixture.Create<string>();
        private readonly string _itemId = _fixture.Create<string>();
        private readonly Command _command = _fixture.Create<Command>();
        private readonly CommandEntity _commandEntity = _fixture.Create<CommandEntity>();

        public CommandServiceTests()
        {
            _commandRepository = Substitute.For<ICommandRepository>();
            _subject = new CommandService(_commandRepository);
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.CreateAsync(_guildId, _command);

            await _commandRepository.Received().CreateAsync(_guildId, _command.Name, Arg.Any<CommandEntity>());
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.DeleteAsync(_guildId, _itemId);

            await _commandRepository.Received().DeleteAsync(_guildId, _itemId);
        }

        [Fact]
        public void GetByItemIdAsync_WhenCalledAndRepositoryReturnsNull_ThenThrowException()
        {
            CommandEntity entity = null;
            _commandRepository.GetByItemIdAsync(default, default).ReturnsForAnyArgs(entity);

            Func<Task> act = async () => await _subject.GetByItemIdAsync(_guildId, _itemId);

            act.Should().Throw<InjhinuityNotFoundWebException>().WithMessage($"Command was not found | GuildId: {_guildId}, Name: {_itemId}");
        }

        [Fact]
        public async Task GetByItemIdAsync_WhenCalledAndRepositoryReturnsEntity_ThenReturnsEntity()
        {
            _commandRepository.GetByItemIdAsync(default, default).ReturnsForAnyArgs(_commandEntity);

            var result = await _subject.GetByItemIdAsync(_guildId, _itemId);

            await _commandRepository.Received().GetByItemIdAsync(_guildId, _itemId);
            result.Should().BeOfType<Command>();
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.UpdateAsync(_guildId, _itemId, _command);

            await _commandRepository.Received().UpdateAsync(_guildId, _itemId, Arg.Any<CommandEntity>());
        }
    }
}
