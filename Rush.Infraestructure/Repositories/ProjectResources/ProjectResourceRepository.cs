using Rush.Domain.Entities.ProjectResources;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.ProjectResources;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;

namespace Rush.Infraestructure.Repositories.ProjectResources
{
    class ProjectResourceRepository: BaseRepository<ProjectResource>, IProjectResourceRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectResourceRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }
    }
}
