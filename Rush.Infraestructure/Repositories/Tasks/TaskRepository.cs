using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Tasks;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;
using Rush.Domain.Common.ViewModels.Tasks;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.DTO.Activities;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.TaskEmployees;

namespace Rush.Infraestructure.Repositories.Tasks
{
    class TaskRepository: BaseRepository<Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }

        public async Task<TaskQW> GetTaskById(Guid id)
        {
            var task = await _context.Tasks
                .Where(t => t.Id == id )
                .Select(t => new TaskQW()
                {
                    Id = t.ProjectId,
                    ProjectId = t.ProjectId,
                    Name = t.Name,
                    Description = t.Description,
                    EstimatedHours = t.EstimatedHours,
                    WorkedHours = t.WorkedHours,
                    StartDate = t.StartDate,
                    EndTime = t.EndTime,
                    TaskEmployees = t.TaskEmployees.Select(te => new TaskEmployees()
                    {
                        EmployeeId = te.EmployeeId,
                        TaskId = te.TaskId,
                        Employee = new Employee()
                        {
                            Id = te.Employee.Id,
                            Name = te.Employee.Name,
                            LastName = te.Employee.LastName
                        }
                    }).ToList(),
                    Activities = t.Activities.Select(a => new Activity()
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Status = a.Status,
                        Employee= new Employee()
                        {
                            Id = a.Employee.Id,
                            Name = a.Employee.Name,
                            LastName = a.Employee.LastName
                        }
                    }).ToList()

                })
                .FirstOrDefaultAsync();

            return task;

        }

        public async Task<TaskEmployees> AssignEmployee(Guid taskId, Guid employeeId)
        {
            var taskEmployee = new TaskEmployees
            {
                TaskId = taskId,
                EmployeeId = employeeId
            };

            _context.TaskEmployees.Add(taskEmployee);
            await _context.SaveChangesAsync();

            return taskEmployee;
        }

        public async Task<List<TaskVM>> GetAllTaskFromProject(Guid ProjectId)
        {
            var task = _context.Tasks
                .Where(t => t.ProjectId == ProjectId)
                .Select(t => new TaskVM
                {
                    Id = t.ProjectId,
                    ProjectId = t.ProjectId,
                    Name = t.Name,
                    Description = t.Description,
                    EstimatedHours = t.EstimatedHours,
                    WorkedHours = t.WorkedHours,
                    StartDate = t.StartDate,
                    EndTime = t.EndTime,
                    Activities = t.Activities.Select(a => new ActivityDTO
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Status = a.Status,
                    }).ToList()

                })
                .ToList();

            return task;

        }
    }
}
