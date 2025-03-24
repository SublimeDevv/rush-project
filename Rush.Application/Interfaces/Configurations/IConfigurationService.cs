using Rush.Domain.Common.ViewModels.Util;

namespace Rush.Application.Interfaces.Configurations
{
    public interface IConfigurationService
    {
        Task<ResponseHelper> GetDiscordToken();
        Task<ResponseHelper> GetSlackToken();
        Task<ResponseHelper> GetEmail();
        Task<ResponseHelper> UpdateDiscordToken(string newToken);
        Task<ResponseHelper> UpdateSlackToken(string newToken);
        Task<ResponseHelper> UpdateEmail(string newEmail);
        void EnsureConfigExists();
    }
}
