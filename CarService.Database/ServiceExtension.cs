using CarService.Database.Repositories;
using CarService.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Database
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services) 
        {
            var mongoConnection = new MongoConnection("mongodb://localhost", "CarService");

            services.AddSingleton(_ => mongoConnection);
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IDefectRepository, DefectRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<ISparePartRepository, SparePartRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
