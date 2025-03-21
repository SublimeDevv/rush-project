using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.DTO.AuditLogs
{
    public class AuditLogDTO: BaseDTO
    {
        public string Message { get; set; }
        public HttpMethodLog HttpMethod { get; set; }
        public string Endpoint { get; set; }
        public LogLevel Level { get; set; }
        public string? UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

}
