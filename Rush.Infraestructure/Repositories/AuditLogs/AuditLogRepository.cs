using Rush.Domain.Entities.Audit;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.AuditLogs;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;

namespace Rush.Infraestructure.Repositories.AuditLogs
{
    class AuditLogRepository : BaseRepository<AuditLog>, IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;
        public AuditLogRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }
    }
}
