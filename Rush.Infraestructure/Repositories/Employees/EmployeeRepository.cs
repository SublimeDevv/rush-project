using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Employees;
using Rush.Infraestructure.Repositories.Generic;
using System.Security.Claims;

namespace Rush.Infraestructure.Repositories.Employees
{
    class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context, ClaimsPrincipal user) : base(context, user)
        {
            _context = context;
        }
    }
}
