using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.Common.ViewModels.Employees
{
    public class ProjectDataByEmployeeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
    }
}
