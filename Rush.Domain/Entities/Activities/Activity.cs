using System.ComponentModel.DataAnnotations.Schema;
using Rush.Domain.Entities.Employees;
using Task = Rush.Domain.Entities.Tasks.Task;
using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.Entities.Activities
{
    [Table("Tbl_Activities")]
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Managment of the foreign keys
        [ForeignKey("TaskId")]
        public Guid TaskId { get; set; }
        public Task? Task { get; set; }

        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }  /*=> En lugar de "GetBy", que sea EmployeeId, queda claro que es el que est� asignado en la actividad*/
        public Employee? Employee { get; set; }

        public StatusActivity Status { get; set; }

    }
}