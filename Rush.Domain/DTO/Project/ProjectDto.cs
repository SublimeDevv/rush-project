using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.DTO.Project
{
    public class ProjectDTO: BaseDTO
    {
        public required string Name { get; set; }

        public string Description { get; set; } = null!;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; } = DateTime.Now;

        public StatusProject Status { get; set; } = StatusProject.ON_HOLD;

    }
}
