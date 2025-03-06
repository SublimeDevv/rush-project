using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Auth;
using Rush.Domain.DTO.Auth;

namespace Rush.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await authService.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await authService.LoginAccount(loginDTO);
            return Ok(response);
        }

    }
}
