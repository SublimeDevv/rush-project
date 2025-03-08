using Rush.Domain.Common.Util;

namespace Rush.Domain.Entities;

public class Project : BaseEntity
{
    
    public required string Name { get; set; }

    public string Description { get; set; } = null!; 
    
    public DateTime StartDate { get; set; } = DateTime.Now;
    
    public DateTime EndTime { get; set; } = DateTime.Now;

    public Enums.StatusProject Status { get; set; } = Enums.StatusProject.ON_HOLD;
    
    //Managment of the foreign keys
    public List<ProjectResources>? ProjectResources { get; set; } = [];
    
    public List<User>? Users { get; set; } = [];
    
    public List<Activities>? Activities { get; set; } = [];
    
    public List<Tasks>? Tasks { get; set; } = [];
    
}