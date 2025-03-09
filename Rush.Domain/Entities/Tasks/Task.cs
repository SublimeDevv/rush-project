using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Projects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities.Tasks
{
    [Table("Tbl_Tasks")]
    public class Task : BaseEntity
    {

        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public required string Name { get; set; }

        public required string Description { get; set; }

        // In an int way for a simple management in minutes
        public int EstimatedHours { get; set; }
        public int WorkedHours { get; set; }

        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndTime { get; set; } = null!;

        public List<Activity> Activities { get; set; } = [];

    }
}

