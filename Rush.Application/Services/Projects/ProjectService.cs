using AutoMapper;
using Rush.Application.Interfaces.Projects;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Project;
using Rush.Domain.Entities.Projects;
using Rush.Infraestructure.Interfaces.Projects;

namespace Rush.Application.Services.Projects
{
    class ProjectService: ServiceBase<Project, ProjectDTO>, IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
