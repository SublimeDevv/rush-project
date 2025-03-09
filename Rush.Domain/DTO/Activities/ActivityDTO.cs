using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.DTO.Activities
{
    public class ActivityDTO: BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusActivity Status { get; set; }
        public Guid TaskId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
