namespace Rush.Application.Interfaces.Employees;

public interface IEmployeeManagementService
{
    public Task<bool> ManageRoleAssignment (Guid employeeId, string? role);
}