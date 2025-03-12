using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetResourceWithProjects")]
        public async Task<IActionResult> GetResourceWithProjects(Guid Id)
        { 
            var result = await _service.GetResourceWithProjects(Id);
            return Ok(result);
        }

        [HttpGet("GetResourcesByproject")]
        public async Task<IActionResult> GetResourcesByproject(Guid Id)
        {
            var result = await _service.GetResourcesByproject(Id);
            return Ok(result);
        }


        [HttpGet("GetDashboardDataResources")]
        public async Task<IActionResult> GetDashboardDataResources()
        {
            var result = await _service.GetDashboardDataResources();
            return Ok(result);
        }

    }
}
