using Rush.Domain.Common.ViewModels.Auth;
using Rush.Domain.DTO.Auth;
using Rush.Domain.Entities;

namespace Rush.Infraestructure.Interfaces.Auth
{
    public interface ITokenRepository
    {
        Task<TokenResponse> GenerateTokens(ApplicationUser user, UserSession userSession);
    }
}
