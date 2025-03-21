using Rush.Domain.DTO.Activities;

namespace Rush.Domain.Common.ViewModels.Tasks
{
    public class TaskVM
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int EstimatedHours { get; set; }
        public int WorkedHours { get; set; }
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndTime { get; set; } = null!;
        public List<ActivityDTO> Activities { get; set; }
    }
}
