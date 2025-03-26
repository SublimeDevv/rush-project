using Rush.Domain.DTO.Tasks;
using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.DTO.Project
{
    public class ProjectDTO : BaseDTO
    {
        public required string Name { get; set; }

        public string Description { get; set; } = null!;

        public StatusProject Status { get; set; } = StatusProject.ON_HOLD;

    }

    public class ProjectForListDTO : BaseDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string EnchargedOf { get; set; }
    }
    
    public class ProjectForDetailDTO : BaseDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string EnchargedOf { get; set; }
        public List<EmployeeDTOForProject> Employees { get; set; }
        public List<TaskForProjectDTO> Tasks { get; set; }
        public List<ResourcesProjectDTO> Resources { get; set; }
    }
    
    public class ResourcesProjectDTO : BaseDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Used { get; set; }
    }
    
    public class TaskForProjectDTO : BaseDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public List<ActivitiesForProjectDTO> Activities { get; set; }
    }

    public class ActivitiesForProjectDTO : BaseDTO
    {
        
    }

    public class EmployeeDTOForProject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }

public class CreateProjectDTO
    {
        public required string Name { get; set; }

        public string Description { get; set; } = null!;

        public StatusProject? Status { get; set; } = StatusProject.ON_HOLD;
        
        public Guid? EmployeeId { get; set; } 
        

    }


}
