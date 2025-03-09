using Rush.Domain.Entities.Activities;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Activities;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Repositories.Activities
{
    public class ActivityRepository: BaseRepository<Activity>, IActivityRepository
    {
        private readonly ApplicationDbContext _context;
        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
