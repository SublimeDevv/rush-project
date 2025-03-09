using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rush.Application.Interfaces.Activities;
using Rush.Application.Interfaces.Employees;
using Rush.Application.Interfaces.ProjectResources;
using Rush.Application.Interfaces.Projects;
using Rush.Application.Interfaces.Resources;
using Rush.Application.Services.Activities;
using Rush.Application.Services.Employees;
using Rush.Application.Services.ProjectResources;
using Rush.Application.Services.Projects;
using Rush.Application.Services.Resources;

namespace Rush.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the configuration for the area that depends from the application project.
    /// </summary>
    /// <param name="services">Application Section</param>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectResourceService, ProjectResourceService>();
        services.AddScoped<IResourceService, ResourceService>();

        return services;
    }
    
}