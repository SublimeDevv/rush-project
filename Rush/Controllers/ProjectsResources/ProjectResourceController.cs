using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.ProjectResources;
using Rush.Domain.DTO.ProjectResources;
using Rush.Domain.Entities.ProjectResources;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.ProjectsResources
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectResourceController: BaseController<ProjectResource, ProjectResourceDTO>
    {
        private readonly IProjectResourceService _service;
        public ProjectResourceController(IProjectResourceService service)
             : base(service)
        {
            _service = service;
        }
    }
}
