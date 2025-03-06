using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;

namespace Rush.Infraestructure.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<ResponseHelper> CreateAccount(UserDTO userDTO);
        Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO);
    }
}
