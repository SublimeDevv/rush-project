using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rush.Domain.Entities;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Interfaces.Activities;
using Rush.Infraestructure.Interfaces.Admin;
using Rush.Infraestructure.Interfaces.AuditLogs;
using Rush.Infraestructure.Interfaces.Auth;
using Rush.Infraestructure.Interfaces.Employees;
using Rush.Infraestructure.Interfaces.ProjectResources;
using Rush.Infraestructure.Interfaces.Projects;
using Rush.Infraestructure.Interfaces.Resources;
using Rush.Infraestructure.Interfaces.Tasks;
using Rush.Infraestructure.Repositories.Activities;
using Rush.Infraestructure.Repositories.Admin;
using Rush.Infraestructure.Repositories.AuditLogs;
using Rush.Infraestructure.Repositories.Auth;
using Rush.Infraestructure.Repositories.Employees;
using Rush.Infraestructure.Repositories.ProjectResources;
using Rush.Infraestructure.Repositories.Projects;
using Rush.Infraestructure.Repositories.Resources;
using Rush.Infraestructure.Repositories.Tasks;
using Rush.Infraestructure.Services.Webhook;

namespace Rush.Infraestructure;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the configuration for the area that depends from the infrastructure project.
    /// </summary>
    /// <param name="services">Infrastructure Section</param>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddIdentity<ApplicationUser, IdentityRole>()
         .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddSignInManager()
         .AddRoles<IdentityRole>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = ctx =>
                {
                   
                    ctx.Request.Cookies.TryGetValue("access_token", out var accessToken);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        ctx.Token = accessToken;
                    }

                    ctx.Request.Cookies.TryGetValue("refresh_token", out var refreshToken);
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        ctx.HttpContext.Items["refresh_token"] = refreshToken;
                    }

                    return Task.CompletedTask;
                }
            };



        });

        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
                
        AddServices(services);
        AddRepository(services);
        
        return services;
    }
    
    /// <summary>
    /// Adds the services.
    /// </summary>
    /// <param name="services">The services.</param>
    private static void AddServices(IServiceCollection services)
    {
        services.AddHttpClient<DiscordWebhookService>();
    }

    /// <summary>
    /// Adds the repository.
    /// </summary>
    /// <param name="services">The repository.</param>
    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectResourceRepository, ProjectResourceRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
    }
}