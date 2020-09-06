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
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _service;
        private readonly IInjhinuityMapper _mapper;

        public CommandController(ICommandService service, IInjhinuityMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("commands")]
        public async Task<IActionResult> CreateAsync(string guildId, [FromBody] CommandRequest? request)
        {
            if (_mapper.Map<CommandRequest, Command>(request) is Command item)
                await _service.CreateAsync(guildId, item);
            else
                throw new InjhinuityBadRequestWebException("Command request was empty or invalid", "Invalid request format");
            
            return Ok();
        }

        [HttpDelete("command/{itemId}")]
        public async Task<IActionResult> DeleteAsync(string guildId, string itemId)
        {
            await _service.DeleteAsync(guildId, itemId);
            return Ok();
        }

        [HttpGet("command/{itemId}")]
        public async Task<IActionResult> GetAsync(string guildId, string itemId)
        {
            var command = await _service.GetByItemIdAsync(guildId, itemId);
            return Ok(_mapper.Map<Command, CommandResponse>(command));
        }

        [HttpGet("commands")]
        public async Task<IActionResult> GetAllAsync(string guildId)
        {
            var commands = await _service.GetAllAsync(guildId);
            return Ok(_mapper.MapEnumerable<Command, CommandResponse>(commands));
        }

        [HttpPut("commands")]
        public async Task<IActionResult> UpdateAsync(string guildId, [FromBody] CommandRequest? request)
        {
            if (_mapper.Map<CommandRequest, Command>(request) is Command item)
                await _service.UpdateAsync(guildId, item.Name, item);
            else
                throw new InjhinuityBadRequestWebException("Command request was empty or invalid", "Invalid request format");

            return Ok();
        }
    }
}
