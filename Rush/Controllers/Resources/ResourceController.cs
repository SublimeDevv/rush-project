using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Resources;
using Rush.Domain.DTO.Resources;
using Rush.Domain.Entities.Resources;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.Resources
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController: BaseController<Resource, ResourceDTO>
    {
        private readonly IResourceService _service;
        public ResourceController(IResourceService service)
             : base(service)
        {
            _service = service;
        }
    }
}
