using Rush.Domain.Entities.Audit;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.AuditLogs
{
    public interface IAuditLogRepository: IBaseRepository<AuditLog>
    {
    }
}
