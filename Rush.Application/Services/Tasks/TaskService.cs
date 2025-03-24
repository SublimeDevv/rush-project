using AutoMapper;
using Rush.Application.Interfaces.Configurations;
using Rush.Application.Interfaces.Tasks;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Common.ViewModels.Tasks;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Tasks;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Interfaces.Tasks;
using Task = Rush.Domain.Entities.Tasks.Task;


namespace Rush.Application.Services.Tasks
{
    class TaskService: ServiceBase<Task, TaskDTO>, ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfigurationService _configurationService;

        public TaskService(ITaskRepository repository, IMapper mapper, IConfigurationService configurationService) : base(mapper, repository, configurationService)
        {
            _mapper = mapper;
            _repository = repository;
            _configurationService = configurationService;
        }

        public async Task<ResponseHelper> GetAllTaskFromProject(Guid ProjectId)
        {
            ResponseHelper response = new();

            try
            {
                List<TaskVM> tasks = await _repository.GetAllTaskFromProject(ProjectId);

                if (tasks.Count == 0)
                {

                    response.Message = "No se encontraron las tareas";
                    response.Success = false;
                }

                response.Data = tasks;
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
