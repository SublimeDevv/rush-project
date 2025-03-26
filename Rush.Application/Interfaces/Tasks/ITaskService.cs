using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Tasks;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.Application.Interfaces.Tasks
{
    public interface ITaskService: IServiceBase<Task, TaskDTO>
    {
        public Task<ResponseHelper> GetAllTaskFromProject(Guid ProjectId);

        public Task<ResponseHelper> GetTaskById(Guid id);

        public Task<ResponseHelper> AssignEmployee(Guid taskId, Guid employeeId);


    }
}
