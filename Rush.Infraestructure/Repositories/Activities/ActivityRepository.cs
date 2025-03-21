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
    public class ActivityRepository: BaseRepository<Activity>, IActivityRepository
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
    }
}
