using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Interfaces.Projects;
using Rush.Domain.Common.Util;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.Projects;
using Rush.WebAPI.Controllers.BaseGeneric;
using Rush.WebAPI.Services;

namespace Rush.WebAPI.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController: BaseController<Project, ProjectDTO>
    {
        private readonly IProjectService _service;
        private readonly IEmployeeService _employeeService;
        public ProjectController(IProjectService service, IEmployeeService employeeService)
             : base(service)
        {
            _service = service;
            _employeeService = employeeService;
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(Guid id, int status)
        {
            
            await _service.ChangeStatus(id, status);
            
            return Ok(new ResponseHelper()
            {
                Success = true,
                Message = "Estado cambiado"
            });
        }
        
        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetStatus()
        {
            ResponseHelper responseHelper = new ResponseHelper();

            string[][] roles = Enum.GetValues(typeof(Enums.StatusProject))
                .Cast<Enums.StatusProject>()
                .Select(r => new string[] { ((int)r).ToString(), r.ToString() })
                .ToArray();
            
            responseHelper.Data = roles;
            responseHelper.Success = true;
            
            return Ok(responseHelper); 
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllM()
        {
            UserClaimsValidator userValidator = new UserClaimsValidator(User);
            
            if (userValidator.CheckForRole(["Gerente", "Admin"]))
            {
                var list = await _service.GetAll();
                
                return Ok(list
                );      
            }

            return Ok(await _service.GetAllForEmployee(userValidator.GetUserId()));
        }
        
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUser()
        {
            
            UserClaimsValidator userValidator = new UserClaimsValidator(User);
            
            var employee = await  _employeeService.GetEmployeeByUserId(userValidator.GetUserId());
            
            return Ok(
                await _service.GetById( employee.ProjectId.Value , userValidator.GetUserId())
            );      
            
        }            
        
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdM(Guid id)
        {

            UserClaimsValidator userValidator = new UserClaimsValidator(User);

            if (userValidator.CheckForRole(["Gerente", "Admin", "Supervisor"]))
            {
                return Ok(
                    await _service.GetById(id)
                );      
            }

            return Ok(
                 await _service.GetById(id, userValidator.GetUserId())
            );      
            
        }
        
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO projectDto)
        {
            await _service.Create(projectDto);
            
            return Ok(new ResponseHelper()
            {
                Success = true,
                Message ="Proyecto creado"
            });
        }
        
        [HttpPut("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] CreateProjectDTO projectDto)
        {
            
            await _service.Update(id, projectDto);
            
            return Ok(new ResponseHelper()
            {
                Success = true,
                Message ="Proyecto creado"
            });
        }
        
        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            await _service.DeleteAndRelease(id);
            
            return Ok(new ResponseHelper()
            {
                Success = true,
                Message ="Proyecto eliminado"
            });
        }
        
    }
}
