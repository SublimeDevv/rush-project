using Microsoft.AspNetCore.Identity;
using Rush.Application.Interfaces.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;
using Rush.Domain.Entities;
using Rush.Infraestructure.Interfaces.Auth;
using Rush.Infraestructure;

namespace Rush.Application.Services.Auth
{
    public class AuthService:  IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepository _authRepository;

        private readonly ApplicationDbContext _context;

        public AuthService(IAuthRepository authRepository,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _authRepository = authRepository;
    
            _userManager = userManager;
            _context = context;
        }

        public async Task<ResponseHelper> CreateAccount(UserDTO userDTO)
        {
            ResponseHelper response = new();
            try
            {
                response = await _authRepository.CreateAccount(userDTO);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO)
        {
            ResponseHelperAuth response = new();
            try
            {
                var result = await _authRepository.LoginAccount(loginDTO);
                if (result.Success)
                {
                    response = result;
                } else
                {
                    response.Message = result.Message;
                }

            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        private string GenerateSlug(string title)
        {
            var slug = title.ToLower().Replace(" ", "-");
            return slug;
        }
    }
}
