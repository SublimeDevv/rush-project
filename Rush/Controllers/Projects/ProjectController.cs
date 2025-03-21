using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Projects;
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
        public ProjectController(IProjectService service)
             : base(service)
        {
            _service = service;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllM()
        {
            UserClaimsValidator userValidator = new UserClaimsValidator(User);
            
            if (userValidator.CheckForRole(["Gerente", "Admin"]))
            {
                return Ok(
                    new ResponseHelper()
                    {
                        Success = true,
                        Message = "Correctamente bien ejecutado",
                        Data = await _service.GetAllAsync()
                    }
                );      
            }

            return Ok(
                new ResponseHelper()
                {
                    Success = true,
                    Message = "Correctamente",
                    Data = await _service.GetAllForEmployee(userValidator.GetUserId()
                    
                    )
                });
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdM(Guid id)
        {

            UserClaimsValidator userValidator = new UserClaimsValidator(User);

            if (userValidator.CheckForRole(["Gerente", "Admin", "Supervisor"]))
            {
                return Ok(
                    new ResponseHelper()
                    {
                        Success = true,
                        Message = "Correctamente bien ejecutado",
                        Data = await _service.GetById(id)
                    }
                );      
            }

            return Ok(
                new ResponseHelper()
                {
                    Success = true,
                    Message = "Correctamente bien ejecutado",
                    Data = await _service.GetById(id, userValidator.GetUserId())
                }
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
        
    }
}
