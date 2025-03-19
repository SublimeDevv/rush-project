using Rush.Domain.Entities.Projects;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Projects;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;

namespace Rush.Infraestructure.Repositories.Projects
{
    class ProjectRepository: BaseRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }
    }
}
