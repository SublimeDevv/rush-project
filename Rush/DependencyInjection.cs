using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Rush.Infraestructure;
using Rush.Application.Mappings;
using Rush.Application.Interfaces.Auth;
using Rush.Application.Services.Auth;
using Rush.Infraestructure.Interfaces.Auth;
using Rush.Application.Services.Seeders;
using Rush.Infraestructure.Interfaces.Ejemplo;
using Rush.Application.Interfaces.Ejemplo;
using Rush.Application.Services.Ejemplo;
using Rush.Infraestructure.Common;
using Rush.Infraestructure.Common.Repositories.Auth;
using Rush.Infraestructure.Common.Repositories.Ejemplo;
using Rush.Infraestructure.Services.Webhook;
using Swashbuckle.AspNetCore.Filters;

namespace Rush.WebAPI
{

    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the dependency injection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
    
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToDtoMappingProfile());
            });
            
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
    
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
    
            });
            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
    
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
    
            
            return services;
        }
    
    }
}
