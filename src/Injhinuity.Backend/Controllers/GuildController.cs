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
    [Route("api/[controller]")]
    public class GuildController : ControllerBase
    {
        private readonly IGuildService _guildService;

        public GuildController(IGuildService guildService)
        {
            _guildService = guildService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GuildRequest request)
        {
            if (request?.Adapt<Guild>() is Guild item)
                await _guildService.CreateAsync(item.Id, item);
            else
                throw new InjhinuityBadRequestWebException("Guild request was empty or invalid");

            return Ok();
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetAsync(string itemId)
        {
            var guild = await _guildService.GetByItemIdAsync(itemId);
            return Ok(guild.Adapt<GuildResponse>());
        }
    }
}
