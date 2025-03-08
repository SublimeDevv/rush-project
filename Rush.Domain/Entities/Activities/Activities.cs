using System.ComponentModel.DataAnnotations.Schema;
using Rush.Domain.Common.Util;

namespace Rush.Domain.Entities;

public class Activities: BaseEntity
{
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    //Managment of the foreign keys
    [ForeignKey("TaskId")]
    public Guid TaskId { get; set; }
    public Tasks? Tasks { get; set; }
    
    [ForeignKey("GetBy")]
    public Guid GetBy { get; set; }
    public Employees? Employees { get; set; }
    
    public Enums.StatusActivity Status { get; set; }
    
}