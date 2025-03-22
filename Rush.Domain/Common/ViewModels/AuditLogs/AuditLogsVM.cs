using Rush.Domain.DTO.AuditLogs;

namespace Rush.Domain.Common.ViewModels.AuditLogs
{
    public class AuditLogsVM: AuditLogDTO
    {
        public string UserEmail { get; set; }
    }
}
