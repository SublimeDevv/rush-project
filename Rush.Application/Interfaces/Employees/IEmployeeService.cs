using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;

namespace Rush.Application.Interfaces.Employees
{
    public interface IEmployeeService: IServiceBase<Employee, EmployeeDTO>
    {
        public Task AssignProject(Guid employeeId, Guid projectId, string? role );

        public Task RemoveProject(Guid employeeId, string? role);
        
    }
}
