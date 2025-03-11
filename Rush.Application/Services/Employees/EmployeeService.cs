using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Interfaces.Employees;

namespace Rush.Application.Services.Employees
{
    public class EmployeeService : ServiceBase<Employee, EmployeeDTO>, IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager) : base(mapper, repository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task AssignProject(Guid employeeId, Guid projectId, string role)
        {
            var employee = await _repository.GetSingleAsync(s => s.Id == employeeId);
            var employeeWithRelations = await _repository.GetSingleWithRelationsAsync(s => s.Id == employeeId);
            
            if (employee == null)
                throw new Exception($"Employee with ID {employeeId} not found");

            if (projectId != Guid.Empty)
            {
                employee.ProjectId = projectId;
            }
            
            var user = (employeeWithRelations.User.Id is not null) ? await _userManager.FindByIdAsync(employeeWithRelations.User.Id) : null;
            
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var rol in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, rol);
                }
                if (!roles.Contains(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            
            await _repository.UpdateAsync(employee);

        }
    }

}
