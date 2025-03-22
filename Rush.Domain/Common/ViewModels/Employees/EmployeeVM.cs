using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.Common.ViewModels.Employees
{
    public class EmployeeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Curp { get; set; }
        public string Rfc { get; set; }
        public string Salary { get; set; }
        public string UserId { get; set; }
        public Sexo Sexo { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
