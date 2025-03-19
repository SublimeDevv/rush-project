using System.ComponentModel.DataAnnotations.Schema;
using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.Entities.Audit
{
    [Table("Tbl_AuditLogs")]
    public class AuditLog: BaseEntity
    {
        public string Message { get; set; }
        public HttpMethodLog HttpMethod { get; set; }
        public string Endpoint { get; set; }
        public LogLevel Level { get; set; }
        public string? UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
