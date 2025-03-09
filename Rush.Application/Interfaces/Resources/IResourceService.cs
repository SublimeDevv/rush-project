using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.Resources;
using Rush.Domain.Entities.Resources;

namespace Rush.Application.Interfaces.Resources
{
    public interface IResourceService: IServiceBase<Resource, ResourceDTO>
    {
    }
}
