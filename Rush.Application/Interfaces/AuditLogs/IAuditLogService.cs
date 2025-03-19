using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.AuditLogs;
using Rush.Domain.Entities.Audit;

namespace Rush.Application.Interfaces.AuditLogs
{
    public interface IAuditLogService: IServiceBase<AuditLog, AuditLogDTO>
    {
    }
}
