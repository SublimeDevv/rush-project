using AutoMapper;
using Rush.Domain.DTO;
using System.Linq.Expressions;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Infraestructure.Repositories.Generic;
using Rush.Application.Extensions;
using Serilog;
using Rush.Application.Interfaces.Base;
using System.Text.Json;
using Rush.Application.Services.Webhook;
using Rush.Application.Interfaces.Configurations;

namespace Rush.Application.Services.Base
{
    public class ServiceBase<T, TDto> : IServiceBase<T, TDto> where T : class where TDto : BaseDTO
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<T> _repository;
        private readonly IConfigurationService _configurationService;

        public ServiceBase(IMapper mapper, IBaseRepository<T> baseRepository, IConfigurationService configurationService)
        {
            _mapper = mapper;
            _configurationService = configurationService;
            _repository = baseRepository;
        }

        private async Task LogsCrud(string message)
        {
            var discordWebhookService = new DiscordWebhookService(new HttpClient());

            var config = await _configurationService.GetDiscordToken();

            string? webhookUrl = config.Data?.ToString();

            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                return;
            }

            string formattedMessage = $"```json\n{message}\n```";

            await discordWebhookService.SendMessageAsync(webhookUrl, formattedMessage);
        }


        private async Task LogsGeneric(string message)
        {
            var discordWebhookService = new DiscordWebhookService(new HttpClient());
            var config = await _configurationService.GetDiscordToken();

            string? webhookUrl = config.Data?.ToString();

            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                return;
            }

            string formattedMessage = $"```json\n{message}\n```";
            await discordWebhookService.SendMessageAsync(webhookUrl, formattedMessage);
        }

        private async Task SlackLog(string message)
        {
            var slackWebhookService = new SlackWebhookService(new HttpClient());
            var config = await _configurationService.GetSlackToken();
            string? webhookUrl = config.Data?.ToString();
            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                return;
            }
            await slackWebhookService.SendMessageAsync(webhookUrl, LogFormatter.FormatAsChatMessage(message));
        }

        private async Task SlackGenericLog(string message)
        {
            var slackWebhookService = new SlackWebhookService(new HttpClient());
            var config = await _configurationService.GetSlackToken();
            string? webhookUrl = config.Data?.ToString();
            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                return;
            }
            await slackWebhookService.SendMessageAsync(webhookUrl, LogFormatter.FormatAsChatMessage(message));

        }

        public async Task<ResponseHelper> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetAllAsync(filter);

                response.Success = true;
                response.Data = data;

                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogsCrud(dataAsJson);
                await SlackLog(dataAsJson);
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
                await SlackGenericLog(e.Message);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }
        
        

        public virtual async Task<ResponseHelper> InsertAsync(T entity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.InsertAsync(entity);

                if (result != Guid.Empty)
                {
                    response.Message = $"El elemento {typeof(T).GetDisplayName()} fue insertado con éxito.";
                    response.Success = true;
                    response.Data = entity;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogsCrud(dataAsJson);
                    await SlackLog(dataAsJson);

                    Log.Information(response.Message);

                    var idProperty = entity.GetType().GetProperty("Id");
                    if (idProperty != null && idProperty.CanWrite)
                    {
                        idProperty.SetValue(entity, result, null);
                    }
                    response.Data = entity;

                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
                await SlackGenericLog(e.Message);
                Log.Error(e.Message);
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseHelper> UpdateAsync(T entity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.UpdateAsync(entity);

                if (result > 0)
                {
                    response.Message = $"El elemento {typeof(T).GetDisplayName()} fue actualizado con éxito.";
                    response.Success = true;
                    response.Data = entity;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogsCrud(dataAsJson);
                    await SlackLog(dataAsJson);

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
                await SlackGenericLog(e.Message);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> RemoveAsync(T entity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.RemoveAsync(entity);

                if (result > 0)
                {
                    response.Message = $"El elemento  {typeof(T).GetDisplayName()} fue eliminado con éxito.";

                    response.Success = true;
                    response.Data = entity; 
                    
                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogsCrud(dataAsJson);
                    await SlackLog(dataAsJson);

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
                await SlackGenericLog(e.Message);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> RemoveAsync(Guid id)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.RemoveAsync(id);

                if (result > 0)
                {
                    response.Message = $"El elemento  {typeof(T).GetDisplayName()} fue eliminado con éxito.";


                    response.Success = true;
                    response.Data = result;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogsCrud(dataAsJson);
                    await SlackLog(dataAsJson);

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
                await SlackGenericLog(e.Message);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetById(Expression<Func<T, bool>>? filter = null)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetSingleAsync(filter);

                response.Success = true;
                response.Data = data;

                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogsCrud(dataAsJson);
                await SlackLog(dataAsJson);

            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
                await SlackGenericLog(e.Message);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<TDto> ConvertToDto(T entity)
        {
            return await Task.FromResult(_mapper.Map<TDto>(entity));
        }
        public async Task<T> ConvertToEntity(TDto dto)
        {
            return await Task.FromResult(_mapper.Map<T>(dto));
        }
        public async Task<List<TDto>> ConvertToDto(List<T> entities)
        {
            return await Task.FromResult(_mapper.Map<List<TDto>>(entities));
        }
        public async Task<List<T>> ConvertToEntity(List<TDto> dto)
        {
            return await Task.FromResult(_mapper.Map<List<T>>(dto));
        }
    }
}
