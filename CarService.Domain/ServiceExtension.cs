using CarService.Domain.Services;
using CarService.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Domain
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
