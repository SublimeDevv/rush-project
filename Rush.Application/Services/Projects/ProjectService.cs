using AutoMapper;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Interfaces.Projects;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;
using Rush.Infraestructure.Interfaces.Employees;
using Rush.Infraestructure.Interfaces.Projects;

namespace Rush.Application.Services.Projects
{
    class ProjectService: ServiceBase<Project, ProjectDTO>, IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IEmployeeManagementService _managementService;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository repository, IMapper mapper, IEmployeeService employeeService, IEmployeeManagementService managementService) : base(mapper, repository)
        {
            _mapper = mapper;
            _managementService = managementService;
            _repository = repository;
        }
        
        public async Task Create(CreateProjectDTO createProjectDto)
        {
            Project project = new Project()
            {
                Name = createProjectDto.Name,
                Description = createProjectDto.Description,
                StartDate = createProjectDto.StartDate,
            };
            
            await _repository.InsertAsync(project);
            
            _managementService.ManageRoleAssignment(createProjectDto.EmployeeId, "Supervisor");
        }
        
    }
}
