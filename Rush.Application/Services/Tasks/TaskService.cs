using AutoMapper;
using Rush.Application.Interfaces.Tasks;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Tasks;
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
    }
}
