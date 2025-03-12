using AutoMapper;
using Rush.Application.Interfaces.Resources;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Resources;
using Rush.Domain.Entities.Projects;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Interfaces.Projects;
using Rush.Infraestructure.Interfaces.Resources;

namespace Rush.Application.Services.Resources
{
    public class ResourceService: ServiceBase<Resource, ResourceDTO>, IResourceService
    {
        private readonly IResourceRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ResourceService(IResourceRepository repository, IMapper mapper, IProjectRepository projectRepository) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _projectRepository = projectRepository;
        }

        public async Task<ResponseHelper> GetResourceWithProjects(Guid Id) {
            ResponseHelper response = new();

            try
            {
                Resource resource = await _repository.GetSingleAsync(x => x.Id == Id);

                if (resource == null) {

                    response.Message = "No se encontró el recurso";
                    response.Success = false;
                }

                List<ResourceWithProjectVM> projects = await _repository.GetResourceWithProjects(Id);

                if (projects == null)
                {
                    response.Message = "No se encontró el recurso";
                    response.Success = false;
                }

                Object results = new { 
                    resource,
                    projects
                };

                response.Data = results;
                response.Success = true;
                    

            } catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetResourcesByproject(Guid Id)
        {
            ResponseHelper response = new();

            try
            {
                Project project = await _projectRepository.GetSingleAsync(x => x.Id == Id);

                if (project == null)
                {

                    response.Message = "No se encontró el proyecto";
                    response.Success = false;
                }

                List<ResourceVM> resources = await _repository.GetResourcesByproject(Id);

                if (resources == null)
                {
                    response.Message = "No se encontraron los recursos";
                    response.Success = false;
                }

                Object results = new
                {
                    project,
                    resources
                };

                response.Data = results;
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetDashboardDataResources()
        {
            ResponseHelper response = new();

            try
            {
                ResourceDataDashboardVM resourcesDataDashboard = await _repository.GetDashboardDataResources();

                if (resourcesDataDashboard == null)
                {
                    response.Message = "No se encontraron los datos";
                    response.Success = false;
                }

                response.Data = resourcesDataDashboard;
                response.Message = "Datos obtenidos con éxito";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
