using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;

namespace Rush.Application.Interfaces.Projects
{
    public interface IProjectService: IServiceBase<Project, ProjectDTO>
    {
    }
}
