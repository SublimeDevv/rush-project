using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rush.Application.Interfaces.Activities;
using Rush.Application.Interfaces.AuditLogs;
using Rush.Application.Interfaces.Auth;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Interfaces.ProjectResources;
using Rush.Application.Interfaces.Projects;
using Rush.Application.Interfaces.Resources;
using Rush.Application.Interfaces.Tasks;
using Rush.Application.Services.Activities;
using Rush.Application.Services.AuditLogs;
using Rush.Application.Services.Auth;
using Rush.Application.Services.Employees;
using Rush.Application.Services.ProjectResources;
using Rush.Application.Services.Projects;
using Rush.Application.Services.Resources;
using Rush.Application.Services.Seeders;
using Rush.Application.Services.Tasks;
using System.Security.Claims;

namespace Rush.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the configuration for the area that depends from the application project.
    /// </summary>
    /// <param name="services">Application Section</param>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectResourceService, ProjectResourceService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IEmployeeManagementService, EmployeeService>();
        services.AddScoped<IAuditLogService, AuditLogService>();
        services.AddHttpContextAccessor();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(s =>
        {
            IHttpContextAccessor contextAccessor = s.GetService<IHttpContextAccessor>();
            ClaimsPrincipal user = contextAccessor?.HttpContext?.User;
            return user ?? throw new Exception("User not resolved");
        });

        services.AddScoped<Seed>();


        return services;
    }
    
}