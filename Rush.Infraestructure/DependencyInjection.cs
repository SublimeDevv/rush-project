using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rush.Domain.Entities;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Common.Repositories.Auth;
using Rush.Infraestructure.Common.Repositories.Ejemplo;
using Rush.Infraestructure.Interfaces.Auth;
using Rush.Infraestructure.Interfaces.Ejemplo;
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddRoles<IdentityRole>();

        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IEjemploRepository, EjemploRepository>();
        
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
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IEjemploRepository, EjemploRepository>();
    }
}