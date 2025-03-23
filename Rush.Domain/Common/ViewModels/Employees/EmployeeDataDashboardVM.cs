using Rush.Domain.Common.ViewModels.Activities;

namespace Rush.Domain.Common.ViewModels.Employees
{
    public class EmployeeDataDashboardVM
    {
        public int PendingTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int TasksAssignedToEmployee { get; set; }
        public List<ActivityVM> LastActivities { get; set; }
        public List<ActivitiesByMonthVM> ActivitiesByMonths { get; set; }
    }
}
