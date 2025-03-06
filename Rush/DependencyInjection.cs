using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rush.Infraestructure;
using Rush.Application.Mappings;
using Rush.Application.Interfaces.Auth;
using Rush.Application.Services.Auth;
using Rush.Infraestructure.Repositories.Auth;
using Rush.Infraestructure.Interfaces.Auth;
using Rush.Application.Services.Seeders;
using Rush.Application.Services.Webhook;
using Rush.Infraestructure.Interfaces.Ejemplo;
using Rush.Infraestructure.Repositories.Ejemplo;
using Rush.Application.Interfaces.Ejemplo;
using Rush.Application.Services.Ejemplo;

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
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            #region DataBaseConnection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            #endregion

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToDtoMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEjemploService, EjemploService>();
            services.AddScoped<Seed>();
           
            services.AddHttpClient<DiscordWebhookService>();
        }

        /// <summary>
        /// Adds the repository.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEjemploRepository, EjemploRepository>();
        }
    }
}
