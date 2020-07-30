using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Injhinuity.Backend.Controllers
{
    [Route("api/guild/{guildId}")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IInjhinuityMapper _mapper;

        public CommandController(ICommandService commandService, IInjhinuityMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        [HttpPost("commands")]
        public async Task<IActionResult> CreateAsync(string guildId, [FromBody] CommandRequest? request)
        {
            if (_mapper.Map<CommandRequest, Command>(request) is Command item)
                await _commandService.CreateAsync(guildId, item);
            else
                throw new InjhinuityBadRequestWebException("Command request was empty or invalid", "Invalid request format");
            
            return Ok();
        }

        [HttpDelete("command/{itemId}")]
        public async Task<IActionResult> DeleteAsync(string guildId, string itemId)
        {
            await _commandService.DeleteAsync(guildId, itemId);
            return Ok();
        }

        [HttpGet("command/{itemId}")]
        public async Task<IActionResult> GetAsync(string guildId, string itemId)
        {
            var command = await _commandService.GetByItemIdAsync(guildId, itemId);
            return Ok(_mapper.Map<Command, CommandRequest>(command));
        }

        [HttpGet("commands")]
        public async Task<IActionResult> GetAllAsync(string guildId)
        {
            var commands = await _commandService.GetAllAsync(guildId);
            return Ok(_mapper.MapEnumerable<Command, CommandRequest>(commands));
        }

        [HttpPut("commands")]
        public async Task<IActionResult> UpdateAsync(string guildId, [FromBody] CommandRequest? request)
        {
            if (_mapper.Map<CommandRequest, Command>(request) is Command item)
                await _commandService.UpdateAsync(guildId, item.Name, item);
            else
                throw new InjhinuityBadRequestWebException("Command request was empty or invalid", "Invalid request format");

            return Ok();
        }
    }
}
