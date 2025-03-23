using AutoMapper;
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
        public TaskService(ITaskRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
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
