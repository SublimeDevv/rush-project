using Rush.Domain.Common.ViewModels.Activities;
using Rush.Domain.DTO.Employees;

namespace Rush.Domain.Common.ViewModels.Employees
{
    public class EmployeeDataDashboardRHVM
    {
        public int ActiveEmployees { get; set; }
        public int EmployeesWithoutProject { get; set; }
        public EmployeeVM EmployeeMostNeedit { get; set; }
        public EmployeeVM LastEmployeeAdded { get; set; }
        public List<EmployeesByMonth> EmployeesByMonths { get; set; }
    }
}
