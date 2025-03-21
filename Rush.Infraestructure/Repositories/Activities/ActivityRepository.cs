using Rush.Domain.Common.ViewModels.Activities;
using Rush.Domain.Common.ViewModels.Tasks;
using Rush.Domain.Entities.Activities;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Activities;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;
using static Rush.Domain.Common.Util.Enums;

namespace Rush.Infraestructure.Repositories.Activities
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        private readonly ApplicationDbContext _context;
        public ActivityRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }

        public async Task<List<ActivityVM>> GetEmployeeActivities(Guid EmployeeId)
        {
            var activities = _context.Activities
                .Where(a => a.EmployeeId == EmployeeId)
                .Select(a => new ActivityVM
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Status = (a.Status).ToString(),
                    Task = new TaskVM
                    {
                        Id = a.Task.Id,
                        Name = a.Task.Name,
                        Description = a.Task.Description,
                    }
                })
                .ToList();

            return activities;

        }

        public async Task<ActivityVM?> MarkAsCompletedActivity(Guid ActivityId)
        {
            var activity = await _context.Activities.FindAsync(ActivityId);
            if (activity == null)
            {
                return null;
            }

            var task = await _context.Tasks.FindAsync(activity.TaskId);
            if (task == null)
            {
                return null;
            }

            activity.Task = task;

            activity.Status = StatusActivity.COMPLETED;
            _context.Activities.Update(activity);
            var result = await _context.SaveChangesAsync();
            Console.WriteLine(result);
            return new ActivityVM
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Status = activity.Status.ToString(),
                Task = task != null ? new TaskVM { Id = task.Id, Name = task.Name, Description = task.Description } : null
            };
        }

    }
}
