
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rush.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the configuration for the area that depends from the application project.
    /// </summary>
    /// <param name="services">Application Section</param>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        
        return services;
    }
    
}