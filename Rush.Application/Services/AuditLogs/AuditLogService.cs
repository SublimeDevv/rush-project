﻿using AutoMapper;
using Rush.Application.Interfaces.AuditLogs;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.AuditLogs;
using Rush.Domain.Entities.Audit;
using Rush.Infraestructure.Interfaces.AuditLogs;


namespace Rush.Application.Services.AuditLogs
{
    public class AuditLogService: ServiceBase<AuditLog, AuditLogDTO>, IAuditLogService
    {

        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _repository;

        public AuditLogService(IAuditLogRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> GetAuditLogs(int? level, int? httpMethod, int offset, int pageSize)
        {


            ResponseHelper response = new();

            try
            {
                var data = await _repository.GetAuditLogs(level, httpMethod, offset, pageSize);

                if (data.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No hay datos que mostrar.";
                    return response;
                }

                response.Message = "Datos obtenidos correctamente.";
                response.Success = true;
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetCountLogs(int level, int httpMethod)
        {
            ResponseHelper response = new();
            try
            {
                var data = await _repository.GetCountLogs(level, httpMethod);
                response.Success = true;
                response.Message = "Datos obtenidos correctamente.";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseHelper> GetAuditEntities()
        {
            ResponseHelper response = new();
            try
            {
                var data = await _repository.GetAuditEntities();

                if (data.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No hay datos que mostrar.";
                    return response;
                }

                response.Success = true;
                response.Message = "Datos obtenidos correctamente.";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseHelper> GetAuditLogsCount()
        {
            ResponseHelper response = new();
            try
            {
                var data = await _repository.GetAuditLogsCount();
                response.Success = true;
                response.Message = "Datos obtenidos correctamente.";
                response.Data = data;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
