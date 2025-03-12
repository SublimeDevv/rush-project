using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Resources;
using Rush.Domain.Entities.Resources;

namespace Rush.Application.Interfaces.Resources
{
    public interface IResourceService: IServiceBase<Resource, ResourceDTO>
    {
        Task<ResponseHelper> GetResourceWithProjects(Guid Id);
        Task<ResponseHelper> GetResourcesByproject(Guid Id);
        Task<ResponseHelper> GetDashboardDataResources();

    }
}
