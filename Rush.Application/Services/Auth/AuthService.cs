using Microsoft.AspNetCore.Identity;
using Rush.Application.Interfaces.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;
using Rush.Domain.Entities;
using Rush.Infraestructure.Interfaces.Auth;
using Rush.Infraestructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Interfaces.Employees;

namespace Rush.Application.Services.Auth
{
    public class AuthService:  IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly ApplicationDbContext _context;

        public AuthService(IAuthRepository authRepository,
            UserManager<ApplicationUser> userManager, IEmployeeRepository employeeRepository, ApplicationDbContext context)
        {
            _authRepository = authRepository;
            _employeeRepository = employeeRepository;
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

        public async Task<ResponseHelper> RegisterEmployee(RegisterEmployeeDTO empleado)
        {
            ResponseHelper response = new();
            try
            {
                UserDTO datosUsuario = new()
                {
                    Email = empleado.Email,
                    Password = empleado.Password,
                    ConfirmPassword = empleado.ConfirmPassword
                };

                var registrarUsuario = await _authRepository.CreateAccount(datosUsuario);

                if (registrarUsuario.Success)
                {
                    var obtenerUsuario = await _userManager.FindByEmailAsync(datosUsuario.Email);
                    if (obtenerUsuario != null)
                    {
                        var usuarioEnDb = _context.Users.Local.FirstOrDefault(u => u.Id == obtenerUsuario.Id);
                        if (usuarioEnDb != null)
                        {
                            _context.Entry(usuarioEnDb).State = EntityState.Detached;
                        }

                        Employee createEmployee = new()
                        {
                            UserId = obtenerUsuario.Id,
                            Name = empleado.Name,
                            LastName = empleado.LastName,
                            Curp = empleado.Curp,
                            Rfc = empleado.Rfc,
                            Salary = empleado.Salary,
                            Age = empleado.Age,
                            Sexo = empleado.Sexo,
                        };

                        var registerEmployee = await _employeeRepository.InsertAsync(createEmployee);
                        if (registerEmployee != Guid.Empty)
                        {
                            response.Message = "Empleado registrado correctamente";
                            response.Success = true;
                        }
                    }
                }
                else
                {
                    response.Message = "El correo ya ha sido registrado";
                    response.Success = false;
                }

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> RefreshToken(string request)
        {
            ResponseHelper response = new();
            try
            {
                response.Data = await _authRepository.RefreshToken(request);
                response.Success = true;
                response.Message = "Token refrescado correctamente.";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;

        }

    }
}
