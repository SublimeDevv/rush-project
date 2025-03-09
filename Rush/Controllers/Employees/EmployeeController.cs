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
    }
    
    
}
