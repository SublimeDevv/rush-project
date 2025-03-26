using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.Configurations;
using Rush.Application.Interfaces.ProjectResources;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.ProjectResources;
using Rush.Domain.Entities.ProjectResources;
using Rush.Infraestructure.Interfaces.ProjectResources;
using Rush.Infraestructure.Interfaces.Resources;
using Serilog;

namespace Rush.Application.Services.ProjectResources
{
    public class ProjectResourceService: ServiceBase<ProjectResource, ProjectResourceDTO>, IProjectResourceService

    {
        private readonly IProjectResourceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfigurationService _configurationService;
        private readonly IResourceRepository _resourceRepository;
        public ProjectResourceService(IProjectResourceRepository repository, IMapper mapper, IConfigurationService configurationService, IResourceRepository resourceRepository) : base(mapper, repository, configurationService)
        {
            _mapper = mapper;
            _repository = repository;
            _configurationService = configurationService;
            _resourceRepository = resourceRepository;
        }
        
        public async Task<ResponseHelper> AssignToProject(Guid projectId, Guid resourceId, int quantity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var resource = await _resourceRepository.GetSingleAsync(x => x.Id == resourceId);
                
                if (resource == null)
                {
                    response.Success = false;
                    response.Message = "Resource not found";
                    return response;
                }
                
                var projectResource = new ProjectResource()
                {
                    ProjectId = projectId,
                    ResourceId = resourceId,
                    Quantity = quantity
                };
                
                await _repository.InsertAsync(projectResource);
                response.Success = true;
                response.Message = "Resource assigned to project";
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }
        
    }
}
