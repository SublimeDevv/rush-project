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
    public class EmployeeService : ServiceBase<Employee, EmployeeDTO>, IEmployeeService, IEmployeeManagementService
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

        public async Task RemoveProject(Guid employeeId, string? role)
        {
            var employee = await _repository.GetSingleAsync(s => s.Id == employeeId);

            if (employee is not null)
            {
                employee.ProjectId = null;
            
                await ManageRoleAssignment(employeeId, role);
            
                await _repository.UpdateAsync(employee);
            }
                
        }
        
        
        public async Task AssignProject(Guid employeeId, Guid projectId, string? role)
        {
            //these i have two queries the firstone is for the model and the second one is to get the userId
            var employee = await _repository.GetSingleAsync(s => s.Id == employeeId);
            
            if (employee == null)
                throw new Exception($"Employee with ID {employeeId} not found");

            if (projectId != Guid.Empty)
            {
                employee.ProjectId = projectId;
            }
            
            await ManageRoleAssignment(employeeId, role);
            
            await _repository.UpdateAsync(employee);

        }
        public async Task<bool> ManageRoleAssignment(Guid employeeId, string? role = "Empleado")
        {
            if(employeeId == Guid.Empty)
                return false;

            var user = await _userManager.FindByIdAsync(employeeId.ToString()); 
            
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                //Validation for the user not be stupid and change the role of the Admin to a Employee
                if(roles.Contains("Admin"))
                {
                    return true;
                }
                
                //These will erase all the roles the user has, later it will have them again (the roles that it need)
                foreach (var rol in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, rol);
                }
                //These will add the asked role
                await _userManager.AddToRoleAsync(user, role);
                
            }
            
            return true;
            
        }
    }

}