using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.ProjectResources;
using static Rush.Domain.Common.Util.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Rush.Domain.Entities.Activities;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.Domain.Entities.Projects
{
    [Table("Tbl_Projects")]
    public class Project : BaseEntity
    {
        public required string Name { get; set; }

        public string Description { get; set; } = null!;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; } = DateTime.Now;

        public StatusProject Status { get; set; } = StatusProject.ON_HOLD;

        //Managment of the foreign keys
        public List<ProjectResource>? ProjectResources { get; set; } = [];

        public List<Employee>? Employee { get; set; } = [];

        public List<Task>? Tasks { get; set; } = [];

    }
}

