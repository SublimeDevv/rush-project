using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;
using Rush.Domain.Entities;
using Rush.Infraestructure.Interfaces.Auth;
using User = Rush.Domain.Entities.User;

namespace Rush.Infraestructure.Common.Repositories.Auth
{
    public class AuthRepository(

        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
        : IAuthRepository
    {

        public async Task<ResponseHelper> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null)
                return new ResponseHelper() { Success = false, Message = "sin datos" };

            var newUser = new User()
            {
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null)
                return new ResponseHelper() { Success = false, Message = "Usuario ya se encuentra registrado" };

            var createUser = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded)
                return new ResponseHelper() { Success = false, Message = "Ha ocurrido un error" };

            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new ResponseHelper() { Success = true, Message = "Cuenta creada con éxito." };
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("Empleado");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Empleado" });

                await userManager.AddToRoleAsync(newUser, "Empleado");
                return new ResponseHelper() { Success = true, Message = "Cuenta creada" };
            }
        }

        public async Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO)
        {

            ResponseHelperAuth response = new();

            if (loginDTO == null)
            {
                response.Success = false;
                response.Message = "No se encuentran los datos";
                return response;
            }

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new ResponseHelperAuth() { Success = false, Message = "Usuario no encontrado" };

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new ResponseHelperAuth() { Success = false, Message = "Usuario o contraseña incorrectos" };

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);

            response.Success = true;
            response.Message = "Acceso correcto";
            response.Token = token;
            response.User.Id = getUser.Id;
            response.User.Rol = getUserRole.First();
            response.User.Email = getUser.Email;
            response.User.AvatarURL = getUser.AvatarURL;

            return response;
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
