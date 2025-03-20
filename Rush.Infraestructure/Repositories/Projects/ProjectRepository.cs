using Microsoft.EntityFrameworkCore;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.ProjectResources;
using Rush.Domain.Entities.Projects;
using Rush.Domain.Entities.TaskEmployees;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Projects;
using Rush.Infraestructure.Repositories.Generic;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.Infraestructure.Repositories.Projects
{
    class ProjectRepository: BaseRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllForEmployee(Guid employeeId)
        {
            var projects = await _context.Projects
                .Include(p => p.Employee) // Asegurar que la relación esté cargada
                .Where(p => p.Employee.Any(e => e.UserId == employeeId.ToString())) // Filtrar primero
                .Select(p => new Project() // Luego proyectar
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndTime = p.EndTime,
                    Employee = p.Employee.Select(e => new Employee()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        LastName = e.LastName,
                        ProjectId = e.ProjectId,
                        User = e.User,
                        UserId = e.UserId
                    }).ToList(),
                })
                .ToListAsync();

            return projects;
        }
        
        public async Task<Project?> GetById(Guid id, Guid userId)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Employee)
                .Include(p => p.ProjectResources)
                .Select(p => new Project()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndTime = p.EndTime,
                    Employee = p.Employee.Select(e => new Employee()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        LastName = e.LastName,
                        ProjectId = e.ProjectId,
                        User = e.User,
                        UserId = e.UserId
                    }).ToList(),
                    Tasks = p.Tasks
                        .Where(t => t.TaskEmployees.Any(te => te.EmployeeId == userId)) // ✅ Reemplazo de Contains()
                        .Select(t => new Task()
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            Activities = t.Activities,
                            ProjectId = t.ProjectId,
                            StartDate = t.StartDate,
                            TaskEmployees = t.TaskEmployees.Select(te => new TaskEmployees()
                            {
                                Id = te.Id,
                                EmployeeId = te.EmployeeId,
                                Employee = new Employee()
                                {
                                    Id = te.Employee.Id,
                                    Name = te.Employee.Name,
                                    LastName = te.Employee.LastName
                                }
                            }).ToList()
                        }).ToList(),
                    ProjectResources = p.ProjectResources.Select(pr => new ProjectResource()
                    {
                        Id = pr.Id,
                        ProjectId = pr.ProjectId,
                        ResourceId = pr.ResourceId,
                        Quantity = pr.Quantity,
                        UsedQuantity = pr.UsedQuantity
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return project;
        }

        public async Task<Project?> GetById(Guid id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Employee)
                .Include(p => p.ProjectResources)
                .Select(p => new Project()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndTime = p.EndTime,
                    Employee = p.Employee.Select(e => new Employee()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        LastName = e.LastName,
                        ProjectId = e.ProjectId,
                        User = e.User,
                        UserId = e.UserId
                    }).ToList(),
                    Tasks = p.Tasks.Select(t => new Task()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Activities = t.Activities,
                        ProjectId = t.ProjectId,
                        StartDate = t.StartDate,
                        TaskEmployees = t.TaskEmployees.Select(te => new TaskEmployees()
                        {
                            Id = te.Id,
                            EmployeeId = te.EmployeeId,
                            Employee = new Employee()
                            {
                                Id = te.Employee.Id,
                                Name = te.Employee.Name,
                                LastName = te.Employee.LastName
                            }
                        }).ToList()
                    }).ToList(),
                    ProjectResources = p.ProjectResources.Select(pr => new ProjectResource()
                    {
                        Id = pr.Id,
                        ProjectId = pr.ProjectId,
                        ResourceId = pr.ResourceId,
                        Quantity = pr.Quantity,
                        UsedQuantity = pr.UsedQuantity
                    }).ToList()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            return project;
        }
    }
}
