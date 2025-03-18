using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;
using Rush.Domain.Entities;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Auth;
using Serilog;

namespace Rush.Infraestructure.Repositories.Auth
{
    class AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        ITokenRepository tokenRepository, ApplicationDbContext context) : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ITokenRepository _tokenRepository = tokenRepository;
        private readonly ApplicationDbContext _context = context;

        public async Task<ResponseHelper> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null)
                return new ResponseHelper() { Success = false, Message = "sin datos" };

            var newUser = new ApplicationUser()
            {
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user is not null)
                return new ResponseHelper() { Success = false, Message = "Usuario ya se encuentra registrado" };

            var createUser = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded)
                return new ResponseHelper() { Success = false, Message = "Ha ocurrido un error" };

            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new ResponseHelper() { Success = true, Message = "Cuenta creada con éxito." };
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("Empleado");
                if (checkUser is null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "Empleado" });

                await _userManager.AddToRoleAsync(newUser, "Empleado");
                return new ResponseHelper() { Success = true, Message = "Cuenta creada" };
            }
        }

        public async Task<ResponseHelper> LoginAccount(LoginDTO loginDTO)
        {

            ResponseHelper response = new();

            if (loginDTO == null)
            {
                response.Success = false;
                response.Message = "No se encuentran los datos";
                return response;
            }

            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new ResponseHelper() { Success = false, Message = "Usuario no encontrado" };

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new ResponseHelper() { Success = false, Message = "Usuario o contraseña incorrectos" };

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Email, getUserRole.First());

            TokenResponse generateTokens = await _tokenRepository.GenerateTokens(getUser, userSession);

            response.Success = true;
            response.Message = "Acceso correcto";
            response.Data = generateTokens;

            return response;
        }

        public async Task<TokenResponse> RefreshToken(string request)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(q => q.RefreshTokenValue == request);

            if (refreshToken is null ||
                refreshToken.Active == false ||
                refreshToken.Expiration <= DateTime.UtcNow)
            {
                throw new ForbiddenAccessException();
            }

            if (refreshToken.Used)
            {
               Log.Error("El refresh token del usuario ya fue usado", refreshToken.UserId, refreshToken.RefreshTokenValue);

                var refreshTokens = await _context.RefreshTokens.Where(q => q.Active && q.Used == false && q.UserId == refreshToken.UserId)
                .ToListAsync();

                foreach (var rt in refreshTokens)
                {
                    rt.Used = true;
                    rt.Active = false;
                }

                await _context.SaveChangesAsync();

                throw new ForbiddenAccessException();
            }

            refreshToken.Used = true;

            var user = await _context.Users.FindAsync(refreshToken.UserId) ?? throw new ForbiddenAccessException();
            var getUserRole = await _userManager.GetRolesAsync(user);

            UserSession userSession = new(user.Id, user.Email, getUserRole.First());

            var generateTokens = await _tokenRepository.GenerateTokens(user, userSession);

            return generateTokens;

        }
    }
}
