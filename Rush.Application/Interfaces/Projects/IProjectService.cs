using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;

namespace Rush.Application.Interfaces.Projects
{
    public interface IProjectService: IServiceBase<Project, ProjectDTO>
    {
        public Task<List<Project?>> GetAll();
        public Task<List<Project?>> GetAllForEmployee(Guid employeeId);
        
        public Task Update(Guid id, CreateProjectDTO createProjectDto);
        public Task Create(CreateProjectDTO createProjectDto);
        public Task<Project?> GetById(Guid id);
        public Task<Project?> GetById(Guid id, Guid userId);

    }
}
