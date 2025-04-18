﻿using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rush.Application.Interfaces.Configurations;
using Rush.Application.Interfaces.Configurations;
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
    public class EmployeeService(IEmployeeRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager, IConfigurationService configurationService) : ServiceBase<Employee, EmployeeDTO>(mapper, repository, configurationService), IEmployeeService, IEmployeeManagementService
    {
        
        private readonly IEmployeeRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IConfigurationService _configurationService = configurationService;
        
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
            
            var rol = await _userManager.GetRolesAsync(_userManager.FindByIdAsync(employee.UserId.ToString()).Result);
            
            if (employee is not null && !rol.Contains("Supervisor"))
            {
                employee.ProjectId = null;
            
                await ManageRoleAssignment(employeeId, "Empleado");
            
                await _repository.UpdateAsync(employee);
            }
                
        }


        public Task<Employee?> GetEmployeeByUserId(Guid UserId)
        {
            return _repository.GetSingleAsync(x => x.UserId == UserId.ToString());
        }

        public async Task AssignProject(Guid employeeId, Guid projectId, string? role)
        {
            if (role is null)
            {
                role = "Empleado";
            }
            
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

        public async Task<bool> ManageRoleAssignment(Guid employeeId , Guid projectId,  string? role) 
        {
            if(employeeId == Guid.Empty)
                return false;
            
            var employee = await _repository.GetSingleAsync(s => s.Id == employeeId);

            if (employee is not null)
            {
                employee.ProjectId = projectId;
            
                await ManageRoleAssignment(employeeId, role);
            
                await _repository.UpdateAsync(employee);
            }
            
            return true;
        }
        public async Task<bool> ManageRoleAssignment(Guid employeeId, string? role) 
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

        public async Task<ResponseHelper> GetRHDasboarData()
        {
            ResponseHelper response = new();

            try
            {
                var employeeRHDashboardData = await _repository.GetRHDasboarData();

                if (employeeRHDashboardData == null)
                {

                    response.Message = "No se encontraron los datos";
                    response.Success = false;
                }

                response.Data = employeeRHDashboardData;
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