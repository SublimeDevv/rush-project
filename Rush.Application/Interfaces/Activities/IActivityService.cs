
using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Activities;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Activities;
using Rush.Domain.Entities.Activities;


namespace Rush.Application.Interfaces.Activities
{
    public interface IActivityService: IServiceBase<Activity, ActivityDTO>
    {
        public Task<ResponseHelper> GetEmployeeActivities(Guid EmployeeId);
        public Task<ResponseHelper> MarkAsCompletedActivity(Guid ActivityId);
    }
}
