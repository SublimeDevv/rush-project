using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.AuditLogs;
using Rush.Domain.Entities.Audit;

namespace Rush.Application.Interfaces.AuditLogs
{
    public interface IAuditLogService: IServiceBase<AuditLog, AuditLogDTO>
    {
        Task<ResponseHelper> GetAuditLogs(int level, int httpMethod, int offset, int pageSize);
        Task<ResponseHelper> GetCountLogs(int level, int httpMethod);
        Task<ResponseHelper> GetAuditEntities();
        Task<ResponseHelper> GetAuditLogsCount();
    }
}
