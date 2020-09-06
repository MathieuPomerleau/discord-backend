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
    public class RoleServiceTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly IRoleService _subject;

        private readonly IRoleRepository _repository;
        private readonly IInjhinuityMapper _mapper;

        private readonly string _guildId = _fixture.Create<string>();
        private readonly string _itemId = _fixture.Create<string>();
        private readonly Role _role = _fixture.Create<Role>();
        private readonly RoleEntity _roleEntity = _fixture.Create<RoleEntity>();
        private readonly IEnumerable<Role> _roles = _fixture.CreateMany<Role>();
        private readonly IEnumerable<RoleEntity> _roleEntities = _fixture.CreateMany<RoleEntity>();

        public RoleServiceTests()
        {
            _repository = Substitute.For<IRoleRepository>();
            _mapper = Substitute.For<IInjhinuityMapper>();
            _mapper.Map<RoleEntity, Role>(default).ReturnsForAnyArgs(_role);
            _mapper.MapEnumerable<RoleEntity, Role>(default).ReturnsForAnyArgs(_roles);
            _mapper.Map<Role, RoleEntity>(default).ReturnsForAnyArgs(_roleEntity);
            _mapper.MapEnumerable<Role, RoleEntity>(default).ReturnsForAnyArgs(_roleEntities);

            _subject = new RoleService(_repository, _mapper);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithANullCommand_ThenThrowException()
        {
            _mapper.Map<Role, RoleEntity>(default).ReturnsForAnyArgs((RoleEntity)null);

            Func<Task> act = async () => await _subject.CreateAsync(_guildId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage($"Provided Role is invalid | GuildId: {_guildId}, Name: null");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithACommand_ThenCallTheRepository()
        {
            await _subject.CreateAsync(_guildId, _role);

            await _repository.Received().CreateAsync(_guildId, _roleEntity.Name, Arg.Any<RoleEntity>());
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.DeleteAsync(_guildId, _itemId);

            await _repository.Received().DeleteAsync(_guildId, _itemId);
        }

        [Fact]
        public void GetByItemIdAsync_WhenCalledAndRepositoryReturnsNull_ThenThrowException()
        {
            _repository.GetByItemIdAsync(default, default).ReturnsForAnyArgs((RoleEntity)null);
            _mapper.Map<RoleEntity, Role>(default).ReturnsForAnyArgs((Role)null);

            Func<Task> act = async () => await _subject.GetByItemIdAsync(_guildId, _itemId);

            act.Should().Throw<InjhinuityNotFoundWebException>().WithMessage($"Role was not found | GuildId: {_guildId}, Name: {_itemId}");
        }

        [Fact]
        public async Task GetByItemIdAsync_WhenCalledAndRepositoryReturnsEntities_ThenReturnsEntities()
        {
            _repository.GetByItemIdAsync(default, default).ReturnsForAnyArgs(_roleEntity);

            var result = await _subject.GetByItemIdAsync(_guildId, _itemId);

            await _repository.Received().GetByItemIdAsync(_guildId, _itemId);
            result.Should().BeOfType<Role>();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledAndRepositoryReturnsEntities_ThenReturnsEntities()
        {
            _repository.GetAllAsync(default).ReturnsForAnyArgs(_roleEntities);

            var result = await _subject.GetAllAsync(_guildId);

            await _repository.Received().GetAllAsync(_guildId);
            result.Should().AllBeOfType<Role>();
        }

        [Fact]
        public void UpdateAsync_WhenCalled_ThenThrowException()
        {
            _mapper.Map<Role, RoleEntity>(default).ReturnsForAnyArgs((RoleEntity)null);

            Func<Task> act = async () => await _subject.UpdateAsync(_guildId, _itemId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage($"Provided Role is invalid | GuildId: {_guildId}, Name: null");
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ThenCallsRepository()
        {
            await _subject.UpdateAsync(_guildId, _itemId, _role);

            await _repository.Received().UpdateAsync(_guildId, _itemId, Arg.Any<RoleEntity>());
        }
    }
}
