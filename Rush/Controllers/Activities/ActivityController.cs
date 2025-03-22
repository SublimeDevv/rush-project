using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Activities;
using Rush.Domain.DTO.Activities;
using Rush.Domain.Entities.Activities;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.Activities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController: BaseController<Activity, ActivityDTO>
    {
        private readonly IActivityService _service;
        public ActivityController(IActivityService service)
             : base(service)
        {
            _service = service;
        }

        [HttpGet("GetEmployeeActivities")]
        public async Task<IActionResult> GetEmployeeActivities(Guid EmployeeId)
        {
            var result = await _service.GetEmployeeActivities(EmployeeId);
            return Ok(result);
        }

        [HttpGet("MarkAsCompletedActivity")]
        public async Task<IActionResult> MarkAsCompletedActivity(Guid ActivityId)
        {
            var result = await _service.MarkAsCompletedActivity(ActivityId);
            return Ok(result);
        }

    }
}
