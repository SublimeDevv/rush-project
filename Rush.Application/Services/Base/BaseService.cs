using AutoMapper;
using Rush.Domain.DTO;
using System.Linq.Expressions;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Infraestructure.Repositories.Generic;
using Rush.Application.Extensions;
using Serilog;
using Rush.Application.Interfaces.Base;
using System.Text.Json;
using Rush.Infraestructure.Services.Webhook;

namespace Rush.Application.Services.Base
{
    public class ServiceBase<T, TDto> : IServiceBase<T, TDto> where T : class where TDto : BaseDTO
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<T> _repository;

        public ServiceBase(IMapper mapper, IBaseRepository<T> baseRepository)
        {
            _mapper = mapper;
            _repository = baseRepository;
        }

        private async Task LogsCrud(string message)
        {
            var discordWebhookService = new DiscordWebhookService(new HttpClient());

            string webhookUrl = "https://discord.com/api/webhooks/1310681662241116252/hxqSonblRdIENS2YveIWt8JmKHbE8u63AKwtaapGLGNMBTBPtm7v-jtzi3NKQY812Kii";

            string formattedMessage = $"```json\n{message}\n```";

            await discordWebhookService.SendMessageAsync(webhookUrl, formattedMessage);
        }


        private async Task LogsGeneric(string message)
        {
            var discordWebhookService = new DiscordWebhookService(new HttpClient());

            string webhookUrl = "https://discord.com/api/webhooks/1310677224197853336/4Ysk48FlYihkS80xyzzZbkqsQs6jLh7TAvmkQYuOSeUwdAnzkhVMvr_kw_Ww4eucGh2s";
            string formattedMessage = $"```json\n{message}\n```";
            await discordWebhookService.SendMessageAsync(webhookUrl, formattedMessage);
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
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
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

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
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

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
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

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
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

            }
            catch (Exception e)
            {
                await LogsGeneric(e.Message);
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
