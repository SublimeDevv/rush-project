using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.Activities;
using Rush.Domain.Common.ViewModels.Employees;
using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Common.ViewModels.Tasks;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Employees;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;
using static Rush.Domain.Common.Util.Enums;

namespace Rush.Infraestructure.Repositories.Employees
{
    class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }

        public async Task<EmployeeDataDashboardVM> GetEmployeeDataDashboard(Guid EmployeeId)
        {
            var lastActivities = _context.Activities.Where(a => a.EmployeeId == EmployeeId && a.IsDeleted == false).Select(a => new ActivityVM
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Status = (a.Status).ToString(),
                CreatedAt = a.CreatedAt,
                Task = new TaskVM
                {
                    Id = a.Task.Id,
                    Name = a.Task.Name,
                    Description = a.Task.Description,
                }
            }).Take(5).ToList();

            string sql = @"
                            -- Actividades pendientes del empleado
                            SELECT COUNT(*) AS pendingTasks FROM Tbl_Activities AS act WHERE EmployeeId = @EmployeeId AND Status = 0 AND IsDeleted = 0;

                            -- Actividades completadas del empleado
                            SELECT COUNT(*) AS completedTasks FROM Tbl_Activities AS act WHERE EmployeeId = @EmployeeId AND Status = 3 AND IsDeleted = 0;

                            -- Tareas a las que pertenece el empleado
                            SELECT COUNT(*) AS tasksAssignedToEmployee FROM Tbl_Activities AS act 
                            INNER JOIN Tbl_Tasks AS tas ON tas.Id = act.TaskId
                            WHERE act.EmployeeId = @EmployeeId AND tas.IsDeleted = 0;

                            -- Actividades asignadas al empleado por mes
                            SELECT YEAR(CreatedAt) AS YEAR, MONTH(CreatedAt) AS MONTH, COUNT(*) AS ActivityCount
                                 FROM Tbl_Activities AS act
                                 WHERE YEAR(CreatedAt) = YEAR(GETDATE()) AND act.EmployeeId = @EmployeeId
                                 GROUP BY YEAR(CreatedAt), MONTH(CreatedAt)
                                 ORDER BY MONTH;";

            using (var multi = await _context.Database.GetDbConnection().QueryMultipleAsync(sql, new { EmployeeId }))
            {
                var pendingTasks = await multi.ReadFirstOrDefaultAsync<int>();
                var completedTasks = await multi.ReadFirstOrDefaultAsync<int>();
                var tasksAssignedToEmployee = await multi.ReadFirstOrDefaultAsync<int>();
                var activitiesByMonths = (await multi.ReadAsync<ActivitiesByMonthVM>()).ToList();

                return new EmployeeDataDashboardVM()
                {
                    PendingTasks = pendingTasks,
                    CompletedTasks = completedTasks,
                    TasksAssignedToEmployee = tasksAssignedToEmployee,
                    ActivitiesByMonths = activitiesByMonths,
                    LastActivities = lastActivities
                };

            }
        }

        public async Task<EmployeeVM> GetEmployeeData(Guid userId)
        {
            string sql = @"SELECT em.*
                            FROM Tbl_Employees AS em
                            INNER JOIN dbo.AspNetUsers AS us ON us.Id = em.UserId
                            WHERE us.Id = @userId";

            using var connection = _context.Database.GetDbConnection();
            var result = await connection.QueryFirstOrDefaultAsync<EmployeeVM>(sql, new { userId });

            return result;
        }

        public async Task<ProjectDataByEmployeeVM> GetEmployeeProject(Guid employeeId)
        {
            string sql = @"SELECT pro.Id, pro.Name, pro.Description, pro.Status, pro.StartDate, pro.EndTime 
                   FROM Tbl_Projects AS pro 
                   INNER JOIN Tbl_Employees AS em 
                   ON pro.Id = em.ProjectId
                   WHERE em.Id = @employeeId";

            using var connection = _context.Database.GetDbConnection();
            var result = await connection.QueryFirstOrDefaultAsync<ProjectDataByEmployeeVM>(sql, new { employeeId });

            if (result != null)
            {
                var statusName = int.Parse(result.Status);
                result.Status = Enum.GetName(typeof(StatusProject), statusName);
            }

            return result;
        }

        public async Task<List<EmployeeProjectDataVM>> GetEmployeesFromProject(Guid projectId)
        {
            string sql = @"SELECT em.Name, em.LastName, us.Email
                            FROM Tbl_Employees AS em
                            INNER JOIN Tbl_Projects AS pro
                            ON pro.Id = em.ProjectId
                            INNER JOIN dbo.AspNetUsers AS us ON us.Id = em.UserId
                            WHERE pro.Id = @projectId";

            using var connection = _context.Database.GetDbConnection();
            var result = await connection.QueryAsync<EmployeeProjectDataVM>(sql, new { projectId });
            return result.ToList();
        }
    }
}
