using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Tasks;
using Rush.Domain.DTO.Tasks;
using Rush.WebAPI.Controllers.BaseGeneric;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.WebAPI.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController: BaseController<Task, TaskDTO>
    {
        private readonly ITaskService _service;
        public TaskController(ITaskService service)
             : base(service)
        {
            _service = service;
        }


        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _service.GetTaskById(id);
            return Ok(result);
        }
        
        [HttpGet("GetAllTaskFromProject")]
        public async Task<IActionResult> GetAllTaskFromProject(Guid ProjectId)
        {
            var result = await _service.GetAllTaskFromProject(ProjectId);
            return Ok(result);
        }
        
        [HttpPost("AssignEmployee")]
        public async Task<IActionResult> AssignEmployee(Guid taskId, Guid employeeId)
        {
            var result = await _service.AssignEmployee(taskId, employeeId);
            return Ok(result);
        }
        
    }
}
