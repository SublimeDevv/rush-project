using Rush.Application.Interfaces.Base;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;

namespace Rush.Application.Interfaces.Employees
{
    public interface IEmployeeService: IServiceBase<Employee, EmployeeDTO>
    {
    }
}
