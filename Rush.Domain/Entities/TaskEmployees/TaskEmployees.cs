
using System.ComponentModel.DataAnnotations.Schema;
using Rush.Domain.Entities.Employees;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.Domain.Entities.TaskEmployees;

public class TaskEmployees: BaseEntity
{
    [ForeignKey("TaskId")]
    public Guid TaskId { get; set; }
    [ForeignKey("EmployeeId")]
    public Guid EmployeeId { get; set; }
    
    public Task? Task { get; set; } = null;
    public Employee? Employee { get; set; } = null; 
}