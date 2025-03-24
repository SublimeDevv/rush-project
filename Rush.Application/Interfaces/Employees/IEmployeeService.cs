using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;

namespace Rush.Application.Interfaces.Employees
{
    public interface IEmployeeService: IServiceBase<Employee, EmployeeDTO>
    {
        
        public Task<Employee?> GetEmployeeByUserId (Guid UserId);
        
        public Task AssignProject(Guid employeeId, Guid projectId, string? role );

        public Task RemoveProject(Guid employeeId, string? role);
        public Task<ResponseHelper> GetEmployeeDataDashboard(Guid EmployeeId);
        public Task<ResponseHelper> GetAllEmployees();
        public Task<ResponseHelper> GetEmployeeData(Guid userId);
        public Task<ResponseHelper> GetEmployeeProject(Guid employeeId);
        public Task<ResponseHelper> GetEmployeesFromProject(Guid projectId);

    }
}
