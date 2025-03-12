using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Employees;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;
using Rush.WebAPI.Controllers.BaseGeneric;

namespace Rush.WebAPI.Controllers.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: BaseController<Employee, EmployeeDTO>
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
             : base(service)
        {
            _service = service;
        }

        [HttpPost("AssignProject")]
        public async Task<IActionResult> AssignProject([FromQuery] EmployeeDTOForProject employeeDto)
        {
            await _service.AssignProject(employeeDto.EmployeeId, employeeDto.ProjectId, employeeDto.Role);
            
            return Ok("Si sirve");
        }

        [HttpPost("RemoveProject")]
        public async Task<IActionResult> RemoveProject([FromQuery] EmployeeDTOForProject employeeDto)
        {
            await _service.RemoveProject(employeeDto.EmployeeId, employeeDto.Role);

            return Ok("Proyecto retirado");
        }
        
    }
    
    
}
