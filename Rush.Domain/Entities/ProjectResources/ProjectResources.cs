using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities;

public class ProjectResources : BaseEntity
{
    
    [ForeignKey("ProjectId")]
    public Guid ProjectId { get; set; }
    
    [ForeignKey("ResourceId")]
    public Guid ResourceId { get; set; }
    public Resources? Resource { get; set; }
    
    public int Quantity { get; set; } 
    public int UsedQuantity { get; set; }
    
}