using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities;

public class EmployeesTasks : BaseEntity
{
    
    [ForeignKey("EmployeesId")]
    public Guid EmployeesId  { get; set; }
    public Employees? Employees { get; set; } = null!;
    
    [ForeignKey("TaskId")]
    public Guid TaskId { get; set; }
    public Tasks? Task { get; set; } = null!;
    
}