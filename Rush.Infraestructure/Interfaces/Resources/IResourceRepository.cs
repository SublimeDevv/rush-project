using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Resources
{
    public interface IResourceRepository: IBaseRepository<Resource>
    {
        Task<List<ResourceWithProjectVM>> GetResourceWithProjects(Guid Id);
        Task<List<ResourceVM>> GetResourcesByproject(Guid Id);
        Task<ResourceDataDashboardVM> GetDashboardDataResources();
    }
}
