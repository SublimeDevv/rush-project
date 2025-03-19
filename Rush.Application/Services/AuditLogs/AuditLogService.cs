using AutoMapper;
using Rush.Application.Interfaces.AuditLogs;
using Rush.Application.Services.Base;
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
    }
}
