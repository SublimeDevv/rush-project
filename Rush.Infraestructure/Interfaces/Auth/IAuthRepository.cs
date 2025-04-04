﻿using Rush.Domain.Common.ViewModels.Auth;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Auth;

namespace Rush.Infraestructure.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<ResponseHelper> CreateAccount(UserDTO userDTO);
        Task<ResponseHelper> LoginAccount(LoginDTO loginDTO);
        Task<TokenResponse> RefreshToken(string request);
    }
}
