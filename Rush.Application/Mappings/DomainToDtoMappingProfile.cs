using AutoMapper;
using Rush.Domain.Entities;
using Rush.Domain.DTO;
using Rush.Domain.Entities.Employees;
using Rush.Domain.DTO.Employees;
using Rush.Domain.DTO.Tasks;
using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Domain.Entities.Projects;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Resources;
using Rush.Domain.DTO.Resources;
using Rush.Domain.Entities.ProjectResources;
using Rush.Domain.DTO.ProjectResources;
using Rush.Domain.Entities.Activities;
using Rush.Domain.DTO.Activities;


namespace Rush.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            this.CreateMap<BaseEntity, BaseDTO>();
            this.CreateMap<Activity, ActivityDTO>().ReverseMap();
            this.CreateMap<Employee, EmployeeDTO>().ReverseMap();
            this.CreateMap<Task, TaskDTO>().ReverseMap();
            this.CreateMap<Project, ProjectDTO>().ReverseMap();
            this.CreateMap<ProjectResource, ProjectResourceDTO>().ReverseMap();
            this.CreateMap<Resource, ResourceDTO>().ReverseMap();

        }
    }
}
