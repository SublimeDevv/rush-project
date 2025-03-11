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

        //public async Task<> GetDataResourcesDashboard()
        //{
        //    string sql = @"";

        //    var result = await _context.Database.GetDbConnection().QueryAsync(sql);
        //}
    }
}
