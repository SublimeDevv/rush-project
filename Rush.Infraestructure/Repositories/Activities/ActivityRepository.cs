using Rush.Domain.Entities.Activities;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Activities;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;

namespace Rush.Infraestructure.Repositories.Activities
{
    public class ActivityRepository: BaseRepository<Activity>, IActivityRepository
    {
        private readonly ApplicationDbContext _context;
        public ActivityRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }
    }
}
