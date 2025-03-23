using AutoMapper;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Interfaces.Projects;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Employees;
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
        
        public async Task<List<Project?>> GetAll()
        {
            var list = await _repository.GetAllAsync();

            list.ToList().OrderBy(c => c.Name);

            return list;
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
