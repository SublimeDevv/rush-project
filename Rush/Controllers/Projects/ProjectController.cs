﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Projects;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;
using Rush.WebAPI.Controllers.BaseGeneric;

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
        
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdMine(Guid id)
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

        
    }
}
