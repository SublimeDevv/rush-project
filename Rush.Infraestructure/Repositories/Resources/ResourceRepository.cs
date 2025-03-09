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
    }
}
