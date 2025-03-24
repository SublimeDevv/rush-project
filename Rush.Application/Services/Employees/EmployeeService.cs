using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Employees;
using Rush.Domain.Common.ViewModels.Resources;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Employees;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.Projects;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Interfaces.Employees;
using Serilog;
using static Rush.Domain.Common.Util.Enums;

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
        
        
        public async Task<ResponseHelper> GetAllEmployees()
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetAllAsync();

                response.Success = true;

                var list = new List<object>();

                foreach (var x in data)
                {
                    var user = await _userManager.FindByIdAsync(x.UserId.ToString());
                    var roles = await _userManager.GetRolesAsync(user);
    
                    list.Add(new { x, Roles = roles });
                }
                
                response.Data = list;
                
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
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

        public async Task<ResponseHelper> GetEmployeeDataDashboard(Guid EmployeeId)
        {
            ResponseHelper response = new();

            try
            {
                var employeeDashboardData = await _repository.GetEmployeeDataDashboard(EmployeeId);

                if (employeeDashboardData == null)
                {

                    response.Message = "No se encontraron los datos";
                    response.Success = false;
                }

                response.Data = employeeDashboardData;
                response.Success = true;


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetEmployeeData(Guid userId)
        {
            ResponseHelper response = new();

            try
            {
                EmployeeVM employee = await _repository.GetEmployeeData(userId);

                if (employee == null)
                {

                    response.Message = "No se encontró el empleado";
                    response.Success = false;
                }

                response.Data = employee;
                response.Success = true;


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetEmployeeProject(Guid employeeId)
        {
            ResponseHelper response = new();

            try
            {
                var project = await _repository.GetEmployeeProject(employeeId);

                if (project == null)
                {

                    response.Message = "No se encontró el proyecto";
                    response.Success = false;
                }

                response.Data = project;
                response.Success = true;


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetEmployeesFromProject(Guid projectId)
        {
            ResponseHelper response = new();

            try
            {
                var employees = await _repository.GetEmployeesFromProject(projectId);

                if (employees == null)
                {

                    response.Message = "No se encontraron empleados para este proyecto";
                    response.Success = false;
                }

                response.Data = employees;
                response.Success = true;


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }

}