namespace Rush.Domain.DTO.Tasks
{
    public class TaskDTO: BaseDTO
    {
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int EstimatedHours { get; set; }
        public int WorkedHours { get; set; }
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndTime { get; set; } = null!;
    }
}
