using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.AuditLogs;
using Rush.Domain.DTO.AuditLogs;
using Rush.Domain.Entities.Audit;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.AuditLogs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController(IAuditLogService service) : BaseController<AuditLog, AuditLogDTO>(service)
    {
        private readonly IAuditLogService _service = service;

        [HttpGet("GetAuditLogs")]
        public async Task<IActionResult> GetAuditLogs([FromQuery] int? level = null, [FromQuery] int? httpMethod = null, int offset = 0, int pageSize = 10)
        {
            var result = await _service.GetAuditLogs(level, httpMethod, offset, pageSize);
            return Ok(result);
        }

        [HttpGet("GetCountLogs")]
        public async Task<IActionResult> GetCountLogs(int level = 0, int httpMethod = 0)
        {
            var result = await _service.GetCountLogs(level, httpMethod);
            return Ok(result);
        }

        [HttpGet("GetAuditEntities")]
        public async Task<IActionResult> GetAuditEntities()
        {
            var result = await _service.GetAuditEntities();
            return Ok(result);
        }

        [HttpGet("GetAuditLogsCount")]
        public async Task<IActionResult> GetAuditLogsCount()
        {
            var result = await _service.GetAuditLogsCount();
            return Ok(result);
        }

    }
}
