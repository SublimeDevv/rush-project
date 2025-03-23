using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Admin;

namespace Rush.WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service) 
        {
            _service = service;
        }

        [HttpGet("GetAdminDashboardData")]
        public async Task<IActionResult> GetAdminDashboardData()
        {
            var result = await _service.GetAdminDashboardData();
            return Ok(result);
        }
    }
}
