using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface ISupplierOrderService
    {
        public Task<BaseResponse<SupplierOrder>> CreateSupplierOrderAsync(SupplierOrder order);

        public Task<BaseResponse<List<SupplierOrderTableItem>>> GetAllSuppliersOrdersAsync();

        public Task<BaseResponse<List<SupplierOrderTableItem>>> GetSuppliersOrdersByDateAsync(DateTime from, DateTime to);

        public Task<BaseResponse<SupplierOrder>> GetSupplierOrderByIdAsync(string id);
    }
}
