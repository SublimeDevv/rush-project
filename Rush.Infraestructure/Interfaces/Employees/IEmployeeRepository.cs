using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Employees
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
    }
}
