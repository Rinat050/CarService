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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IDefectRepository, DefectRepository>();
            services.AddScoped<ISupplierOrderRepository, SupplierOrderRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<ISparePartRepository, SparePartRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

            return services;
        }
    }
}
