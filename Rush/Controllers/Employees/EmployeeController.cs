using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Employees;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.Projects;
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

        [HttpGet("GetAllWithoutProject")]
        public async Task<IActionResult> GetAllWithoutProject()
        {
            var list = await _service.GetAllAsync(s => s.ProjectId == null);
            return Ok(list);
        }
        
        [HttpPost("AssignProject")]
        public async Task<IActionResult> AssignProject([FromBody] EmployeeDTOForProject employeeDto)
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

        [HttpGet("GetEmployeeDataDashboard")]
        public async Task<IActionResult> GetEmployeeDataDashboard(Guid EmployeeId)
        {
            var result = await _service.GetEmployeeDataDashboard(EmployeeId);
            return Ok(result);
        }

        [HttpGet("GetEmployeeData")]
        public async Task<IActionResult> GetEmployeeData(Guid userId)
        {
            var result = await _service.GetEmployeeData(userId);
            return Ok(result);
        }

        [HttpGet("GetEmployeeProject")]
        public async Task<IActionResult> GetEmployeeProject(Guid employeeId)
        {
            var result = await _service.GetEmployeeProject(employeeId);
            return Ok(result);
        }

        [HttpGet("GetEmployeesFromProject")]
        public async Task<IActionResult> GetEmployeesFromProject(Guid projectId)
        {
            var result = await _service.GetEmployeesFromProject(projectId);
            return Ok(result);
        }

    }
    
    
}
