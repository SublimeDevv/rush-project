using AutoMapper;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities.Employees;
using Rush.Infraestructure.Interfaces.Employees;

namespace Rush.Application.Services.Employees
{
    public class EmployeeService: ServiceBase<Employee, EmployeeDTO>, IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
