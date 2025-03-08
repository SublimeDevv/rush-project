using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities;

public class Tasks : BaseEntity
{
    
    [ForeignKey("ProjectId")]
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; } = null!;
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    // In an int way for a simple management in minutes
    public int EstimatedHours { get; set; }
    public int WorkedHours { get; set; }

    public DateTime? StartDate { get; set; } = null!; 
    public DateTime? EndTime { get; set; }= null!; 
    
    public List<Activities> Activities { get; set; } = [];
    
    public List<Employees> Employees { get; set; } = [];
    
}