using AutoMapper;
using Rush.Application.Interfaces.ProjectResources;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.ProjectResources;
using Rush.Domain.Entities.ProjectResources;
using Rush.Infraestructure.Interfaces.ProjectResources;

namespace Rush.Application.Services.ProjectResources
{
    public class ProjectResourceService: ServiceBase<ProjectResource, ProjectResourceDTO>, IProjectResourceService

    {
        private readonly IProjectResourceRepository _repository;
        private readonly IMapper _mapper;
        public ProjectResourceService(IProjectResourceRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
