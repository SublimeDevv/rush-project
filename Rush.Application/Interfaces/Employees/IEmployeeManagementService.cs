namespace Rush.Application.Interfaces.Employees;

public interface IEmployeeManagementService
{
    public Task<bool> ManageRoleAssignment (Guid employeeId, Guid projectId , string? role = "Supervisor");
    public Task<bool> ManageRoleAssignment (Guid employeeId, string? role = "Empleado");
}