using Dapper;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.AuditLogs;
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

        public async Task<List<AuditLogsVM>> GetAuditLogs(int level, int httpMethod, int offset, int pageSize)
        {
            string sql = @"SELECT al.*, asp.Email AS UserEmail
                FROM Tbl_AuditLogs AS al
                LEFT JOIN AspNetUsers AS asp ON asp.Id = al.UserId
                WHERE al.IsDeleted = 0

                  AND (@Level IS NULL OR al.Level = @Level)
                  AND (@HttpMethod IS NULL OR al.HttpMethod = @HttpMethod)
                ORDER BY al.Id DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            var result = await _context.Database.GetDbConnection().QueryAsync<AuditLogsVM>(sql, new { Level = level, HttpMethod = httpMethod, Offset = offset, PageSize = pageSize });
            return result.ToList();
            
        }

        public async Task<int> GetCountLogs(int level, int httpMethod)
        {
            string sql = @"SELECT COUNT(*) AS TotalLogs 
                FROM Tbl_AuditLogs AS al
                WHERE (@HttpMethod IS NULL OR al.HttpMethod = @HttpMethod)
                  AND (@Level IS NULL OR al.Level = @Level)";
            var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(sql, new { Level = level, HttpMethod = httpMethod });
            return result;
        }

        public async Task<List<AuditChangesVM>> GetAuditEntities()
        {
            string sql = @"SELECT ac.*, asp.Email AS UserEmail 
                FROM Tbl_AuditChanges AS ac
                LEFT JOIN AspNetUsers AS asp ON asp.Id = ac.[User]
                WHERE ac.IsDeleted = 0";

            var result = await _context.Database.GetDbConnection().QueryAsync<AuditChangesVM>(sql);
            return result.ToList();
        }

        public async Task<AuditLogsCountVM> GetAuditLogsCount()
        {
            string sql = @"SELECT 
                SUM(CASE WHEN alog.Level = 0 THEN 1 ELSE 0 END) AS InformationCount,
                SUM(CASE WHEN alog.Level = 1 THEN 1 ELSE 0 END) AS WarningCount,
                SUM(CASE WHEN alog.Level = 2 THEN 1 ELSE 0 END) AS ErrorCount,
                SUM(CASE WHEN alog.Level = 3 THEN 1 ELSE 0 END) AS SuccessCount
            FROM Tbl_AuditLogs AS alog 
            WHERE alog.IsDeleted = 0;";
            var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<AuditLogsCountVM>(sql);
            return result;
        }
    }
}
