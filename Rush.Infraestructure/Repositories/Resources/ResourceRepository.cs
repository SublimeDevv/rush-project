using Dapper;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Resources;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Repositories.Resources
{
    class ResourceRepository: BaseRepository<Resource>, IResourceRepository
    {
        private readonly ApplicationDbContext _context;
        public ResourceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<ResourceWithProjectVM>> GetResourceWithProjects(Guid Id)
        {
            string sql = @"SELECT pro.Name, prores.UsedQuantity FROM Tbl_Projects AS pro 
                              INNER JOIN Tbl_ProjectResources AS prores ON pro.Id = prores.ProjectId
                              INNER JOIN Tbl_Resources AS res ON res.Id = prores.ResourceId AND res.IsDeleted = 0
                              WHERE res.Id = @Id AND pro.IsDeleted = 0";
            var result = await _context.Database.GetDbConnection().QueryAsync<ResourceWithProjectVM>(sql, new { Id });
            return result.ToList();
        }

        public async Task<List<ResourceVM>> GetResourcesByproject(Guid Id)
        {
            string sql = @" SELECT res.* FROM Tbl_ProjectResources AS prores
                      INNER JOIN Tbl_Projects AS proj ON proj.Id = prores.ProjectId AND proj.IsDeleted = 0
                      INNER JOIN Tbl_Resources AS res ON res.Id = prores.ResourceId AND res.IsDeleted = 0
                      WHERE proj.Id = @Id AND prores.IsDeleted = 0";

            var result = await _context.Database.GetDbConnection().QueryAsync<ResourceVM>(sql, new { Id });
            return result.ToList();
        }

        public async Task<ResourceDataDashboardVM> GetDashboardDataResources()
        {
            string sql = @"-- Total de recursos
                           SELECT SUM(Quantity) AS totalResources FROM Tbl_Resources AS res WHERE res.IsDeleted = 0;

                           -- Recursos asignados
                           SELECT SUM(UsedQuantity) AS assignedResources FROM Tbl_ProjectResources AS prores WHERE prores.IsDeleted = 0;

                           -- Número de proyectos con recursos asignados
                           SELECT COUNT(*) AS projectsWithResources FROM Tbl_Projects AS pro
                           LEFT JOIN Tbl_ProjectResources AS prores ON pro.Id = prores.ProjectId
                           WHERE prores.IsDeleted = 0;

                           -- Recurso con la mayor cantidad asignada
                           SELECT TOP 1 res.Name FROM Tbl_ProjectResources AS prores
                           LEFT JOIN Tbl_Resources AS res ON prores.ResourceId = res.Id AND prores.IsDeleted = 0
                           WHERE prores.UsedQuantity = (SELECT MAX(UsedQuantity) FROM Tbl_ProjectResources WHERE IsDeleted = 0);
                                        
                           -- Recursos por mes
                           SELECT YEAR(CreatedAt) AS YEAR, MONTH(CreatedAt) AS MONTH, MAX(Quantity) AS ResourceQuantityMonth
                                FROM Tbl_Resources
                                WHERE YEAR(CreatedAt) = YEAR(GETDATE())
                                GROUP BY YEAR(CreatedAt), MONTH(CreatedAt)
                                ORDER BY MONTH;";

            using (var multi = await _context.Database.GetDbConnection().QueryMultipleAsync(sql)) {
                var totalResources = await multi.ReadFirstOrDefaultAsync<int>();
                var assignedResources = await multi.ReadFirstOrDefaultAsync<int>();
                var projectsWithResources = await multi.ReadFirstOrDefaultAsync<int>();
                var mostUsedResource = await multi.ReadFirstOrDefaultAsync<string>();
                var resourcesByMonth = (await multi.ReadAsync<ResourcesByMonthVM>()).ToList();

                return new ResourceDataDashboardVM()
                {
                    TotalResources = totalResources,
                    AssignedResources = assignedResources,
                    ProjectsWithResources = projectsWithResources,
                    MostUsedResource = mostUsedResource,
                    ResourcesByMonth = resourcesByMonth
                };

            }
        }
    }
}
