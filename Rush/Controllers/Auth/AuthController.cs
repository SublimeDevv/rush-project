using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Auth;
using Rush.Domain.Common.ViewModels.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;
using Rush.Domain.Entities;

namespace Rush.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterEmployeeDTO employee)
        {
            var response = await authService.RegisterEmployee(employee);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await authService.LoginAccount(loginDTO);

            TokenResponse? getToken = response.Data as TokenResponse;

            if (response.Success)
            {
                SetTokenCookie("access_token", getToken.AccessToken, 60 * 24 * 7);
                SetTokenCookie("refresh_token", getToken.RefreshToken, 60 * 24 * 7);
            }

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {

            if (!HttpContext.Request.Cookies.TryGetValue("refresh_token", out var refreshToken))
            {
                return BadRequest("Error: No se encontró el token de refresco.");
            }

            var responseRefresh = await authService.RefreshToken(refreshToken);

            if (!responseRefresh.Success)
            {
                return BadRequest(responseRefresh);
            }

            if (responseRefresh.Data is not TokenResponse tokenResponse)
            {
                return BadRequest(responseRefresh);
            }

            SetTokenCookie("access_token", tokenResponse.AccessToken, 40); 
            SetTokenCookie("refresh_token", tokenResponse.RefreshToken, 60 * 24 * 7);

            return Ok(responseRefresh);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            ResponseHelper response = new() { Success = true, Message = "Logout exitoso" };

            if (!HttpContext.Request.Cookies.TryGetValue("refresh_token", out var refreshToken))
            {
                return BadRequest("Error: No se encontró el token de refresco.");
            }

            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");

            return Ok(response);
        }

        [HttpGet("validate-session")]
        public async Task<IActionResult> ValidateSession()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                    return Ok(new ResponseHelper // Es Ok porque sino tira puro error el front en la consola
                    {
                        Success = false,
                        Message = "Sesión no válida"
                    });

                var roles = await _userManager.GetRolesAsync(user);

                return Ok(new ResponseHelper
                {
                    Success = true,
                    Message = $"Sesión valida con: {user.UserName}",
                    Data = new User 
                    {
                        Id = user.Id,
                        Name = user.UserName,
                        Email = user.Email,
                        Rol = roles.FirstOrDefault(),
                        AvatarURL = user.AvatarURL
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }
        
        private void SetTokenCookie(string name, string token, int expireMinutes, string path = "/")
        {
            Response.Cookies.Append(
                name,
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(expireMinutes),
                    Path = path
                }
            );
        }

    }
}
