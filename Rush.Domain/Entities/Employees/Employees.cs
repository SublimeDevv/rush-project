using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities;

public class Employees : BaseEntity
{
    
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    [ForeignKey("ProjectId")]
    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; } = null!;
    
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    
    public string Curp { get; set; }
    public string Rfc { get; set; }
    
    public string Salary { get; set; }
    
    public List<Activities> Activities { get; set; } = [];
    
}