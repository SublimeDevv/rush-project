namespace Rush.Domain.Entities;

public class Resources : BaseEntity
{
    
    public required string  Name { get; set; }
    public required string Description { get; set; }

    public int Quantity { get; set; }
    
    public List<ProjectResources> ProjectResources { get; set; } = [];
}