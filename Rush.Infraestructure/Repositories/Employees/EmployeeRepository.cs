using Dapper;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.Employees;
using Rush.Domain.Common.ViewModels.Resources;
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
