using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.Tasks;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.Application.Interfaces.Tasks
{
    public interface ITaskService: IServiceBase<Task, TaskDTO>
    {
    }
}
