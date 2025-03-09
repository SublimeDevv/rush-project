using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.ProjectResources;
using Rush.Domain.Entities.ProjectResources;

namespace Rush.Application.Interfaces.ProjectResources
{
    public interface IProjectResourceService: IServiceBase<ProjectResource, ProjectResourceDTO>
    {
    }
}
