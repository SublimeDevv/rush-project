using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Projects;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController: BaseController<Project, ProjectDTO>
    {
        private readonly IProjectService _service;
        public ProjectController(IProjectService service)
             : base(service)
        {
            _service = service;
        }
    }
}
