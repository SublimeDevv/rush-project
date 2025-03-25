using AutoMapper;
using Rush.Application.Interfaces.Configurations;
using Microsoft.AspNetCore.Identity;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Interfaces.Projects;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.Projects;
using Rush.Infraestructure.Interfaces.Employees;
using Rush.Infraestructure.Interfaces.Projects;

namespace Rush.Application.Services.Projects
{
    class ProjectService: ServiceBase<Project, ProjectDTO>, IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeManagementService _managementService;
        private readonly IMapper _mapper;
        private readonly IConfigurationService _configurationService;

        public ProjectService(UserManager<ApplicationUser> userManager, IProjectRepository repository, IMapper mapper, IEmployeeService employeeService, IEmployeeManagementService managementService, IConfigurationService configurationService) : base(mapper, repository, configurationService)
        {
            _mapper = mapper;
            _managementService = managementService;
            _userManager = userManager;
            _repository = repository;
            _configurationService = configurationService;
        }
        
        public async Task<ResponseHelper> GetAll()
        {
            ResponseHelper responseHelper = new ResponseHelper(); 

            var list = await _repository.GetAllYes();

            list.ToList().OrderBy(c => c.Name);
            
            responseHelper.Data = await TransformList(list);
            responseHelper.Success = true;
            
            return responseHelper;
        }
        
        public async Task<ResponseHelper> GetAllForEmployee(Guid employeeId)
        {
            ResponseHelper responseHelper = new ResponseHelper(); 
            
            List<Project> list = await _repository.GetAllForEmployee(employeeId);
            
            responseHelper.Data = await TransformList(list);
            responseHelper.Success = true;
            
            return responseHelper;
        }

        private async Task<object> TransformList(List<Project> projects)
        {
            List<object> employeeList = new List<object>();

            foreach (var project in projects)
            {
                var employees = new List<object>();
                var encharge = new List<object>();

                foreach (var y in project.Employee)
                {
                    // Capturamos los datos fuera del contexto de EF Core
                    var user = await _userManager.FindByIdAsync(y.UserId.ToString());
                    var roles = await _userManager.GetRolesAsync(user);

                    employees.Add(new { Employee = y, Roles = roles });
                    
                    if (roles.Contains("Supervisor"))
                    {
                        encharge.Add(new { Employee = y, Roles = roles });
                    }
                }

                employeeList.Add(new { Project = project, Employees = employees, Encharge = encharge,  Status = project.Status.ToString() });
            }

            return employeeList;
        }

        public async Task Create(CreateProjectDTO createProjectDto)
        {
            Project project = new Project()
            {
                Name = createProjectDto.Name,
                Description = createProjectDto.Description,
            };
            
            await _repository.InsertAsync(project);
            
            if (Guid.TryParse(createProjectDto.EmployeeId.ToString(), out Guid employeeId))
            {
                _managementService.ManageRoleAssignment(employeeId, "Supervisor");
            }
            
        }
        
        public async Task Update(Guid id, CreateProjectDTO createProjectDto)
        {
            var project = await _repository.GetSingleAsync(s => s.Id == id);
    
            project.Name = createProjectDto.Name;
            project.Description = createProjectDto.Description;

            project.Employee = new List<Employee>(); 
            project.Tasks = new List<Domain.Entities.Tasks.Task>();
            project.ProjectResources = new List<Domain.Entities.ProjectResources.ProjectResource>();

            
            await _repository.UpdateAsync(project);
            
            if (Guid.TryParse(createProjectDto.EmployeeId.ToString(), out Guid employeeId))
            {
                _managementService.ManageRoleAssignment(employeeId, "Supervisor");
            }

        }
        
        public async Task<ResponseHelper> GetById(Guid id)
        {
            ResponseHelper responseHelper = new ResponseHelper(); 
            
            var project = await _repository.GetById(id);
                        
            responseHelper.Data = await Transform(project);
            responseHelper.Success = true;
            
            return responseHelper;
        }

        public async Task<ResponseHelper> GetById(Guid id, Guid userId)
        {
            ResponseHelper responseHelper = new ResponseHelper(); 

            var project = await _repository.GetById(id, userId);
                        
            responseHelper.Data = await Transform(project);
            responseHelper.Success = true;
            
            return responseHelper;
        }

        private async Task<object> Transform(Project project)
        {

            var employees = new List<object>();
                var encharge = new List<object>();

            foreach (var y in project.Employee)
            {
                // Capturamos los datos fuera del contexto de EF Core
                var user = await _userManager.FindByIdAsync(y.UserId.ToString());
                var roles = await _userManager.GetRolesAsync(user);

                employees.Add(new { Employee = y, Roles = roles });
                
                if (roles.Contains("Supervisor"))
                {
                    encharge.Add(new { Employee = y, Roles = roles });
                }
            }
            
            return new { Project = project, Employees = employees, Encharge = encharge,  Status = project.Status.ToString()};
        }
        
    }
}
