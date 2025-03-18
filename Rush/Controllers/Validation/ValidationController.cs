using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Rush.Application.Interfaces.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.Entities;

namespace Rush.WebAPI.Controllers.Validation;

 [Route("api/[controller]")]
    [ApiController]
    public class ValidationController(IAuthService authService, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet("get-role")]
        public IActionResult GetRoleName()
        {
            var claims = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList().FirstOrDefault();

            return Ok(new ResponseHelper()
            {
                Success = true,
                Message = claims,
                Data = claims
            });
        }

    }