namespace Rush.Domain.Common.ViewModels.Admin
{
    public class AdminDashboardDataVM
    {
        public int activeusers {get; set;}
        public int totalProjects { get; set; }
        public int totalResources { get; set; }
        public int totalLogsByDay { get; set; }
        public List<DataByMonthVM> logsByMonths { get; set; }
        public List<DataByMonthVM> resourcesByMonths { get; set; }
        public List<DataByMonthVM> projectsByMonths { get; set; }
    }
}
