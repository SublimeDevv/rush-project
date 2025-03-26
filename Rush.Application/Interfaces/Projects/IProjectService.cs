using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;

namespace Rush.Application.Interfaces.Projects
{
    public interface IProjectService: IServiceBase<Project, ProjectDTO>
    {
        public Task<ResponseHelper> GetAll();
        
        public Task ChangeStatus(Guid id, int status);
        
        public Task<ResponseHelper> GetAllForEmployee(Guid employeeId);
        public Task Update(Guid id, CreateProjectDTO createProjectDto);
        public Task Create(CreateProjectDTO createProjectDto);
        public Task<ResponseHelper> GetById(Guid id);
        public Task<ResponseHelper> GetById(Guid id, Guid userId);

        public Task DeleteAndRelease(Guid id);
    }
}
