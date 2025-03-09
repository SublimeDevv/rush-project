using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Projects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities.Employees
{
    [Table("Tbl_Employees")]
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string Curp { get; set; }
        public string Rfc { get; set; }

        public string Salary { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        [ForeignKey("ProjectId")]
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; } = null!;
        public List<Activity> Activities { get; set; } = [];

    }
}

