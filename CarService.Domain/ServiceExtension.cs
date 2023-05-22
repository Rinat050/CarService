using CarService.Domain.Services;
using CarService.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Domain
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IDefectService, DefectService>();
            services.AddScoped<IRepairService, RepairService>();
            services.AddScoped<ISparePartService, SparePartService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ICarService, CarService.Domain.Services.CarService>();
            services.AddScoped<ISupplierOrderService, SupplierOrderService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}
