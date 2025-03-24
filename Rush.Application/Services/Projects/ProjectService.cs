using AutoMapper;
using Rush.Application.Interfaces.Configurations;
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
        private readonly IConfigurationService _configurationService;

        public ProjectService(IProjectRepository repository, IMapper mapper, IEmployeeService employeeService, IEmployeeManagementService managementService, IConfigurationService configurationService) : base(mapper, repository, configurationService)
        {
            _mapper = mapper;
            _managementService = managementService;
            _repository = repository;
            _configurationService = configurationService;
        }
        
        public async Task<List<Project?>> GetAllForEmployee(Guid employeeId)
        {
            return await _repository.GetAllForEmployee(employeeId);
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
        
        public async Task<Project?> GetById(Guid id)
        {
            var project = await _repository.GetById(id);

            return project;
        }

        public async Task<Project?> GetById(Guid id, Guid userId)
        {
            var project = await _repository.GetById(id, userId);

            return project;
        }

    }
}
