using Rush.Domain.Common.ViewModels.Activities;
using Rush.Domain.Entities.Activities;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Activities
{
    public interface IActivityRepository : IBaseRepository<Activity>
    {
        public Task<List<ActivityVM>> GetEmployeeActivities(Guid EmployeeId);

    }
}
