using Rush.Domain.Common.ViewModels.Employees;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Employees
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        public Task<EmployeeVM> GetEmployeeData(Guid userId);

        public Task<ProjectDataByEmployeeVM> GetEmployeeProject(Guid employeeId);
        public Task<List<EmployeeProjectDataVM>> GetEmployeesFromProject(Guid projectId);


    }
}
