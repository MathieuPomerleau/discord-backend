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
    public class CommandServiceTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly ICommandService _subject;

        private readonly ICommandRepository _commandRepository;
        private readonly IInjhinuityMapper _mapper;

        private readonly string _guildId = _fixture.Create<string>();
        private readonly string _itemId = _fixture.Create<string>();
        private readonly Command _command = _fixture.Create<Command>();
        private readonly CommandEntity _commandEntity = _fixture.Create<CommandEntity>();
        private readonly IEnumerable<Command> _commands = _fixture.CreateMany<Command>();
        private readonly IEnumerable<CommandEntity> _commandEntities = _fixture.CreateMany<CommandEntity>();

        public CommandServiceTests()
        {
            _commandRepository = Substitute.For<ICommandRepository>();
            _mapper = Substitute.For<IInjhinuityMapper>();
            _mapper.Map<CommandEntity, Command>(default).ReturnsForAnyArgs(_command);
            _mapper.MapEnumerable<CommandEntity, Command>(default).ReturnsForAnyArgs(_commands);
            _mapper.Map<Command, CommandEntity>(default).ReturnsForAnyArgs(_commandEntity);
            _mapper.MapEnumerable<Command, CommandEntity>(default).ReturnsForAnyArgs(_commandEntities);

            _subject = new CommandService(_commandRepository, _mapper);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithANullCommand_ThenThrowException()
        {
            _mapper.Map<Command, CommandEntity>(default).ReturnsForAnyArgs((CommandEntity)null);

            Func<Task> act = async () => await _subject.CreateAsync(_guildId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage($"Provided Command is invalid | GuildId: {_guildId}, Name: null");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithACommand_ThenCallTheRepository()
        {
            await _subject.CreateAsync(_guildId, _command);

            await _commandRepository.Received().CreateAsync(_guildId, _commandEntity.Name, Arg.Any<CommandEntity>());
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
            _commandRepository.GetByItemIdAsync(default, default).ReturnsForAnyArgs((CommandEntity)null);
            _mapper.Map<CommandEntity, Command>(default).ReturnsForAnyArgs((Command)null);

            Func<Task> act = async () => await _subject.GetByItemIdAsync(_guildId, _itemId);

            act.Should().Throw<InjhinuityNotFoundWebException>().WithMessage($"Command was not found | GuildId: {_guildId}, Name: {_itemId}");
        }

        [Fact]
        public async Task GetByItemIdAsync_WhenCalledAndRepositoryReturnsEntities_ThenReturnsEntities()
        {
            _commandRepository.GetByItemIdAsync(default, default).ReturnsForAnyArgs(_commandEntity);

            var result = await _subject.GetByItemIdAsync(_guildId, _itemId);

            await _commandRepository.Received().GetByItemIdAsync(_guildId, _itemId);
            result.Should().BeOfType<Command>();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledAndRepositoryReturnsEntities_ThenReturnsEntities()
        {
            _commandRepository.GetAllAsync(default).ReturnsForAnyArgs(_commandEntities);

            var result = await _subject.GetAllAsync(_guildId);

            await _commandRepository.Received().GetAllAsync(_guildId);
            result.Should().AllBeOfType<Command>();
        }

        [Fact]
        public void UpdateAsync_WhenCalled_ThenThrowException()
        {
            _mapper.Map<Command, CommandEntity>(default).ReturnsForAnyArgs((CommandEntity)null);

            Func<Task> act = async () => await _subject.UpdateAsync(_guildId, _itemId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage($"Provided Command is invalid | GuildId: {_guildId}, Name: null");
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.UpdateAsync(_guildId, _itemId, _command);

            await _commandRepository.Received().UpdateAsync(_guildId, _itemId, Arg.Any<CommandEntity>());
        }
    }
}
