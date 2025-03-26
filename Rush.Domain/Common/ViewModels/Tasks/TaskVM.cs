using Rush.Domain.DTO.Activities;
using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.TaskEmployees;

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
        
        public List<TaskEmployees> TaskEmployees { get; set; }
    }
    
    public class TaskQW
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int EstimatedHours { get; set; }
        public int WorkedHours { get; set; }
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndTime { get; set; } = null!;
        public List<ActivityQw> Activities { get; set; }
        public List<TaskEmployees> TaskEmployees { get; set; }
    }

    public class ActivityQw
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TaskId { get; set; }
        public Task? Task { get; set; }
        public Employee? Employee { get; set; }
        public string Status { get; set; }
    }
}