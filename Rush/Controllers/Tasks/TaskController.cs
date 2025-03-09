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
    }
}
