using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Injhinuity.Backend.Controllers;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Reponses;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Injhinuity.Backend.Tests.Controllers
{
    public class RoleControllerTests
    {
        private static readonly IFixture _fixture = new Fixture();
        private readonly RoleController _subject;

        private readonly IRoleService _service;
        private readonly IInjhinuityMapper _mapper;

        private readonly string _guildId = _fixture.Create<string>();
        private readonly string _itemId = _fixture.Create<string>();
        private readonly Role _role = _fixture.Create<Role>();
        private readonly IEnumerable<Role> _roles = _fixture.CreateMany<Role>();
        private readonly RoleRequest _roleRequest = _fixture.Create<RoleRequest>();
        private readonly RoleResponse _roleResponse = _fixture.Create<RoleResponse>();
        private readonly IEnumerable<RoleResponse> _roleResponses = _fixture.CreateMany<RoleResponse>();

        public RoleControllerTests()
        {
            _service = Substitute.For<IRoleService>();
            _mapper = Substitute.For<IInjhinuityMapper>();
            _mapper.Map<RoleRequest, Role>(default).ReturnsForAnyArgs(_role);
            _mapper.Map<Role, RoleResponse>(default).ReturnsForAnyArgs(_roleResponse);
            _mapper.MapEnumerable<RoleRequest, Role>(default).ReturnsForAnyArgs(_roles);
            _mapper.MapEnumerable<Role, RoleResponse>(default).ReturnsForAnyArgs(_roleResponses);

            _subject = new RoleController(_service, _mapper);
        }

        [Fact]
        public void CreateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            _mapper.Map<RoleRequest, Role>(default).ReturnsForAnyArgs((Role)null);

            Func<Task> act = async () => await _subject.CreateAsync(_guildId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Role request was empty or invalid");
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.CreateAsync(_guildId, _roleRequest);

            await _service.Received().CreateAsync(_guildId, Arg.Any<Role>());
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ThenCallsItsService()
        {
            await _subject.DeleteAsync(_guildId, _itemId);

            await _service.Received().DeleteAsync(_guildId, _itemId);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ThenReturnsARole()
        {
            _service.GetByItemIdAsync(default, default).ReturnsForAnyArgs(_role);

            var result = await _subject.GetAsync(_guildId, _itemId) as OkObjectResult;

            await _service.Received().GetByItemIdAsync(_guildId, _itemId);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ThenReturnsAListOfRoles()
        {
            _service.GetAllAsync(default).ReturnsForAnyArgs(_roles);

            var result = await _subject.GetAllAsync(_guildId) as OkObjectResult;

            await _service.Received().GetAllAsync(_guildId);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public void UpdateAsync_WhenCalledWithNullRequest_ThenThrowsException()
        {
            _mapper.Map<RoleRequest, Role>(default).ReturnsForAnyArgs((Role)null);

            Func<Task> act = async () => await _subject.UpdateAsync(_guildId, null);

            act.Should().Throw<InjhinuityBadRequestWebException>().WithMessage("Role request was empty or invalid");
        }

        [Fact]
        public async Task UpdateAsync_WhenCalledWithCorrectRequest_ThenCallsItsService()
        {
            await _subject.UpdateAsync(_guildId, _roleRequest);

            await _service.Received().UpdateAsync(_guildId, _role.Name, Arg.Any<Role>());
        }
    }
}
