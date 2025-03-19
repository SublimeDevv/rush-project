using Rush.Domain.Entities.Projects;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Projects
{
    public interface IProjectRepository: IBaseRepository<Project>
    {
        public Task<Project?> GetById(Guid id);
    }
}
