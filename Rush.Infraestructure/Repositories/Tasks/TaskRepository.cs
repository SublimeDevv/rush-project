using Task = Rush.Domain.Entities.Tasks.Task;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Tasks;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Repositories.Tasks
{
    class TaskRepository: BaseRepository<Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
