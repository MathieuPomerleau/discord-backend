using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Reponses;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Injhinuity.Backend.Controllers
{
    [Route("api/guild/{guildId}")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        private readonly IInjhinuityMapper _mapper;

        public RoleController(IRoleService service, IInjhinuityMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateAsync(string guildId, [FromBody] RoleRequest? request)
        {
            if (_mapper.Map<RoleRequest, Role>(request) is Role item)
                await _service.CreateAsync(guildId, item);
            else
                throw new InjhinuityBadRequestWebException("Role request was empty or invalid", "Invalid request format");

            return Ok();
        }

        [HttpDelete("role/{itemId}")]
        public async Task<IActionResult> DeleteAsync(string guildId, string itemId)
        {
            await _service.DeleteAsync(guildId, itemId);
            return Ok();
        }

        [HttpGet("role/{itemId}")]
        public async Task<IActionResult> GetAsync(string guildId, string itemId)
        {
            var role = await _service.GetByItemIdAsync(guildId, itemId);
            return Ok(_mapper.Map<Role, RoleResponse>(role));
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllAsync(string guildId)
        {
            var roles = await _service.GetAllAsync(guildId);
            return Ok(_mapper.MapEnumerable<Role, RoleResponse>(roles));
        }

        [HttpPut("roles")]
        public async Task<IActionResult> UpdateAsync(string guildId, [FromBody] RoleRequest? request)
        {
            if (_mapper.Map<RoleRequest, Role>(request) is Role item)
                await _service.UpdateAsync(guildId, item.Name, item);
            else
                throw new InjhinuityBadRequestWebException("Role request was empty or invalid", "Invalid request format");

            return Ok();
        }
    }
}
