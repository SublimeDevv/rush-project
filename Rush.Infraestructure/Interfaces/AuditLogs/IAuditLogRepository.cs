using Rush.Domain.Common.ViewModels.AuditLogs;
using Rush.Domain.Entities.Audit;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.AuditLogs
{
    public interface IAuditLogRepository: IBaseRepository<AuditLog>
    {
        Task<List<AuditLogsVM>> GetAuditLogs(int? level, int? httpMethod, int offset, int pageSize);
        Task<int> GetCountLogs(int level, int httpMethod);
        Task<List<AuditChangesVM>> GetAuditEntities();
        Task<AuditLogsCountVM> GetAuditLogsCount();
    }
}
