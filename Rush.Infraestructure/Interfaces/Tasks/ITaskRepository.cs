using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Infraestructure.Repositories.Generic;
using Rush.Domain.Common.ViewModels.Tasks;
using Rush.Domain.Entities.TaskEmployees;

namespace Rush.Infraestructure.Interfaces.Tasks
{
    public interface ITaskRepository: IBaseRepository<Task>
    {
        public Task<List<TaskVM>> GetAllTaskFromProject(Guid ProjectId);

        public Task<TaskQW> GetTaskById(Guid id);
        
        public Task<TaskEmployees> AssignEmployee(Guid taskId, Guid employeeId);
    }
}
