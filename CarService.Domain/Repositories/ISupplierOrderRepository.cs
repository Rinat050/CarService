using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface ISupplierOrderRepository
    {
        public Task AddSupplierOrderAsync(SupplierOrder supplierOrder);

        public Task<List<SupplierOrderTableItem>> GetAllSuppliersOrdersAsync();

        public Task<List<SupplierOrderTableItem>> GetSuppliersOrdersByDateAsync(DateTime from, DateTime to);

        public Task<SupplierOrder> GetSupplierOrderByIdAsync(string id);
    }
}
