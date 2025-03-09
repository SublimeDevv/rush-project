using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Tasks
{
    public interface ITaskRepository: IBaseRepository<Task>
    {
    }
}
