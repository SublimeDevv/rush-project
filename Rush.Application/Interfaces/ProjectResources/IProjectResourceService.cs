using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.ProjectResources;
using Rush.Domain.Entities.ProjectResources;

namespace Rush.Application.Interfaces.ProjectResources
{
    public interface IProjectResourceService: IServiceBase<ProjectResource, ProjectResourceDTO>
    {
        public Task<ResponseHelper> AssignToProject(Guid projectId, Guid resourceId, int quantity);

    }
}
