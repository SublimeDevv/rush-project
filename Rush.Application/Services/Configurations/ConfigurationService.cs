using Microsoft.EntityFrameworkCore;
using Rush.Application.Interfaces.Configurations;
using Rush.Domain.Common.ViewModels.JSONModels;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.Entities.Configurations;
using Rush.Infraestructure.Common;
using System.Text.Json;

namespace Rush.Application.Services.Configurations
{
    class ConfigurationService(ApplicationDbContext context): IConfigurationService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<ResponseHelper> GetDiscordToken()
        {
            ResponseHelper response = new();
            var config = await _context.Configurations.FirstOrDefaultAsync(c => c.Name == "General");
            if (config != null)
            {
                response.Success = true;
                response.Message = "Token de Discord obtenido correctamente";
                response.Data = config.GetValue<string>(nameof(ConfigurationFileVM.DiscordToken));
                return response;
            }

            response.Success = false;
            response.Message = "No se encontró la configuración";
            return response;
        }

        public async Task<ResponseHelper> GetSlackToken()
        {
            ResponseHelper response = new();
            var config = await _context.Configurations.FirstOrDefaultAsync(c => c.Name == "General");
            if (config != null)
            {
                response.Success = true;
                response.Message = "Token de Slack obtenido correctamente";
                response.Data = config.GetValue<string>(nameof(ConfigurationFileVM.SlackToken));
                return response;
            }

            response.Success = false;
            response.Message = "No se encontró la configuración";
            return response;
        }

        public async Task<ResponseHelper> GetEmail()
        {
            ResponseHelper response = new();
            var config = await _context.Configurations.FirstOrDefaultAsync(c => c.Name == "General");
            if (config != null)
            {
                response.Success = true;
                response.Message = "Email obtenido correctamente";
                response.Data = config.GetValue<string>(nameof(ConfigurationFileVM.Email));
                return response;
            }

            response.Success = false;
            response.Message = "No se encontró la configuración";
            return response;
        }

        public async Task<ResponseHelper> UpdateDiscordToken(string newToken)
        {
            ResponseHelper response = new();
            var config = _context.Configurations.FirstOrDefault(c => c.Name == "General");
            if (config != null)
            {
                config.SetValue(nameof(ConfigurationFileVM.DiscordToken), newToken);
                _context.Entry(config).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Token de Discord actualizado correctamente";
                return response;
            }

            response.Success = false;
            response.Message = "No se encontró la configuración";
            return response;
        }

        public async Task<ResponseHelper> UpdateSlackToken(string newToken)
        {
            ResponseHelper response = new();
            var config = _context.Configurations.FirstOrDefault(c => c.Name == "General");
            if (config != null)
            {
                config.SetValue(nameof(ConfigurationFileVM.SlackToken), newToken);
                _context.Entry(config).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Token de Slack actualizado correctamente";
                return response;
            }

            response.Success = false;
            response.Message = "No se encontró la configuración";
            return response;
        }

        public async Task<ResponseHelper> UpdateEmail(string newEmail)
        {
            ResponseHelper response = new();
            var config = _context.Configurations.FirstOrDefault(c => c.Name == "General");
            if (config != null)
            {
                config.SetValue(nameof(ConfigurationFileVM.Email), newEmail);
                _context.Entry(config).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Email actualizado correctamente";
                return response;
            }

            response.Success = false;
            response.Message = "No se encontró la configuración";
            return response;
        }

        public void EnsureConfigExists()
        {
            if (!_context.Configurations.Any(c => c.Name == "General"))
            {
                var newConfig = new Configuration
                {
                    Name = "General",
                    ConfigurationJson = JsonSerializer.Serialize(new ConfigurationFileVM())
                };
                _context.Configurations.Add(newConfig);
                _context.SaveChanges();
            }
        }
    }
}
