using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Configurations;
using Rush.Domain.Common.ViewModels.Util;

namespace Rush.WebAPI.Controllers.Configurations
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfigurationService _configService;

        public ConfigurationsController(IConfigurationService configService)
        {
            _configService = configService;
            _configService.EnsureConfigExists();
        }

        [HttpGet("{property}")]
        public async Task<IActionResult> Get(string property)
        {
            ResponseHelper response = new();

            switch (property.ToLower())
            {
                case "discord-token":
                    response = await _configService.GetDiscordToken();
                    break;
                case "slack-token":
                    response = await _configService.GetSlackToken();
                    break;
                case "email":
                    response = await _configService.GetEmail();
                    break;
                default:
                    response.Success = false;
                    response.Message = "Propiedad no válida";
                    return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPut("{property}")]
        public async Task<IActionResult> Update(string property, [FromBody] string value)
        {
            ResponseHelper response;

            switch (property.ToLower())
            {
                case "discord-token":
                    response = await _configService.UpdateDiscordToken(value);
                    break;
                case "slack-token":
                    response = await _configService.UpdateSlackToken(value);
                    break;
                case "email":
                    response = await _configService.UpdateEmail(value);
                    break;
                default:
                    return BadRequest("Propiedad no válida");
            }

            return Ok(response);
        }


    }
}
