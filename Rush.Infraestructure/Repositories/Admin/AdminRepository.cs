using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.Admin;
using Rush.Domain.Common.ViewModels.Employees;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Admin;

namespace Rush.Infraestructure.Repositories.Admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardDataVM> GetAdminDashboardData()
        {
            string sql = @"
                            -- Seleccionar los usuarios totales
                            SELECT COUNT(*) AS activeUsers FROM dbo.AspNetUsers as us
                            WHERE us.IsDeleted = 0;

                            -- Seleccionar proyectos totales
                            SELECT COUNT(*) AS totalProjects FROM Tbl_Projects as pro
                            WHERE pro.IsDeleted = 0;

                            -- Seleccionar recursos totales
                            SELECT COUNT(*) AS totalResources FROM Tbl_Resources as res
                            WHERE res.IsDeleted = 0;

                            -- Seleccionar logs totales
                            SELECT COUNT(*) AS totalLogsByDay FROM Tbl_AuditLogs as logs
                            WHERE logs.IsDeleted = 0 AND DAY(logs.CreatedAt) = DAY(GETDATE());

                            -- Logs por mes
                            SELECT YEAR(CreatedAt) AS YEAR, MONTH(CreatedAt) AS MONTH, COUNT(*) AS dataQuantity
                                FROM Tbl_AuditLogs AS logs
                                WHERE YEAR(CreatedAt) = YEAR(GETDATE())
                                GROUP BY YEAR(CreatedAt), MONTH(CreatedAt)
                                ORDER BY MONTH;

                            -- recursos por mes
                            SELECT YEAR(CreatedAt) AS YEAR, MONTH(CreatedAt) AS MONTH, COUNT(*) AS dataQuantity
                                FROM Tbl_Resources AS res
                                WHERE YEAR(CreatedAt) = YEAR(GETDATE())
                                GROUP BY YEAR(CreatedAt), MONTH(CreatedAt)
                                ORDER BY MONTH;

                            -- proyectos por mes
                            SELECT YEAR(CreatedAt) AS YEAR, MONTH(CreatedAt) AS MONTH, COUNT(*) AS dataQuantity
	                            FROM Tbl_Projects AS res
                                WHERE YEAR(CreatedAt) = YEAR(GETDATE())
                                GROUP BY YEAR(CreatedAt), MONTH(CreatedAt)
                                ORDER BY MONTH;";

            using (var multi = await _context.Database.GetDbConnection().QueryMultipleAsync(sql))
            {
                var activeusers = await multi.ReadFirstOrDefaultAsync<int>();
                var totalProjects = await multi.ReadFirstOrDefaultAsync<int>();
                var totalResources = await multi.ReadFirstOrDefaultAsync<int>();
                var totalLogsByDay = await multi.ReadFirstOrDefaultAsync<int>();
                var logsByMonths = (await multi.ReadAsync<DataByMonthVM>()).ToList();
                var resourcesByMonths = (await multi.ReadAsync<DataByMonthVM>()).ToList();
                var projectsByMonths = (await multi.ReadAsync<DataByMonthVM>()).ToList();

                return new AdminDashboardDataVM()
                {
                    activeusers = activeusers,
                    totalProjects = totalProjects,
                    totalResources = totalResources,
                    totalLogsByDay=totalLogsByDay,
                    logsByMonths = logsByMonths,
                    resourcesByMonths = resourcesByMonths,
                    projectsByMonths = projectsByMonths
                };

            }
        }
    }
}
