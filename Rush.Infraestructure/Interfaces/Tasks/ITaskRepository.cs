using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Infraestructure.Repositories.Generic;
using Rush.Domain.Common.ViewModels.Tasks;

namespace Rush.Infraestructure.Interfaces.Tasks
{
    public interface ITaskRepository: IBaseRepository<Task>
    {
        public Task<List<TaskVM>> GetAllTaskFromProject(Guid ProjectId);

    }
}
