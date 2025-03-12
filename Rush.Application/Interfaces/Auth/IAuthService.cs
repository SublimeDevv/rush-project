using Rush.Domain.Common.ViewModels.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;

namespace Rush.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ResponseHelper> CreateAccount(UserDTO userDTO);
        Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO);
        Task<ResponseHelper> RegisterEmployee(RegisterEmployeeDTO employee);
        Task<ResponseHelper> RefreshToken(string request);


    }
}
