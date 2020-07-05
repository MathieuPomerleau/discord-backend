using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Reponses;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Injhinuity.Backend.Controllers
{
    [Route("api/guild/{guildId}")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;

        public CommandController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost("[controller]")]
        public async Task<IActionResult> CreateAsync(string guildId, [FromBody] CommandRequest? request)
        {
            if (request?.Adapt<Command>() is Command item)
                await _commandService.CreateAsync(guildId, item);
            else
                throw new InjhinuityBadRequestWebException("Command request was empty or invalid");
            
            return Ok();
        }

        [HttpGet("[controller]/{itemId}")]
        public async Task<IActionResult> GetAsync(string guildId, string itemId)
        {
            var command = await _commandService.GetByItemIdAsync(guildId, itemId);
            return Ok(command.Adapt<CommandResponse>());
        }
    }
}
