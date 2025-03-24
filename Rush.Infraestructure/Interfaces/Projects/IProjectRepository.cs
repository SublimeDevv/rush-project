using Rush.Domain.Entities.Projects;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Projects
{
    public interface IProjectRepository: IBaseRepository<Project>
    {
        public Task<List<Project>> GetAllYes();
        public Task<List<Project>> GetAllForEmployee(Guid employeeId);
        
        public Task<Project?> GetById(Guid id);
        
        public Task<Project?> GetById(Guid id, Guid userId);

    }
}
