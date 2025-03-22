using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Tasks;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;
using Rush.Domain.Common.ViewModels.Tasks;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.DTO.Activities;

namespace Rush.Infraestructure.Repositories.Tasks
{
    class TaskRepository: BaseRepository<Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
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
