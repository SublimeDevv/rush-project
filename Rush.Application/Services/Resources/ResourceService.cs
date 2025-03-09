using AutoMapper;
using Rush.Application.Interfaces.Resources;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Resources;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Interfaces.Resources;

namespace Rush.Application.Services.Resources
{
    public class ResourceService: ServiceBase<Resource, ResourceDTO>, IResourceService
    {
        private readonly IResourceRepository _repository;
        private readonly IMapper _mapper;
        public ResourceService(IResourceRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
