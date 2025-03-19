using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.AuditLogs;
using Rush.Domain.DTO.AuditLogs;
using Rush.Domain.Entities.Audit;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.AuditLogs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : BaseController<AuditLog, AuditLogDTO>
    {
        private readonly IAuditLogService _service;
        public AuditLogController(IAuditLogService service)
             : base(service)
        {
            _service = service;
        }

    }
}
