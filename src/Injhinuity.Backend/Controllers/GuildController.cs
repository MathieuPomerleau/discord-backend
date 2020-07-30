using System.Threading.Tasks;
using Injhinuity.Backend.Core.Exceptions.Web;
using Injhinuity.Backend.Model.Domain;
using Injhinuity.Backend.Model.Domain.Requests;
using Injhinuity.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Injhinuity.Backend.Controllers
{
    [Route("api")]
    public class GuildController : ControllerBase
    {
        private readonly IGuildService _guildService;
        private readonly IInjhinuityMapper _mapper;

        public GuildController(IGuildService guildService, IInjhinuityMapper mapper)
        {
            _guildService = guildService;
            _mapper = mapper;
        }

        [HttpPost("guilds")]
        public async Task<IActionResult> CreateAsync([FromBody] GuildRequest? request)
        {
            if (_mapper.Map<GuildRequest, Guild>(request) is Guild item)
                await _guildService.CreateAsync(item.Id, item);
            else
                throw new InjhinuityBadRequestWebException("Guild request was empty or invalid", "Invalid request format");

            return Ok();
        }

        [HttpDelete("guild/{itemId}")]
        public async Task<IActionResult> DeleteAsync(string itemId)
        {
            await _guildService.DeleteAsync(itemId);
            return Ok();
        }

        [HttpGet("guild/{itemId}")]
        public async Task<IActionResult> GetAsync(string itemId)
        {
            var guild = await _guildService.GetByItemIdAsync(itemId);
            return Ok(_mapper.Map<Guild, GuildRequest>(guild));
        }

        [HttpGet("guilds")]
        public async Task<IActionResult> GetAllAsync()
        {
            var guilds = await _guildService.GetAllAsync();
            return Ok(_mapper.MapEnumerable<Guild, GuildRequest>(guilds));
        }

        [HttpPut("guilds")]
        public async Task<IActionResult> UpdateAsync([FromBody] GuildRequest? request)
        {
            if (_mapper.Map<GuildRequest, Guild>(request) is Guild item)
                await _guildService.UpdateAsync(item.Id, item);
            else
                throw new InjhinuityBadRequestWebException("Guild request was empty or invalid", "Invalid request format");

            return Ok();
        }
    }
}
