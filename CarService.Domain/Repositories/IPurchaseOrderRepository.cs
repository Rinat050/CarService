using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IPurchaseOrderRepository
    {
        public Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder);

        public Task<List<PurchaseOrderTableItem>> GetAllPurchaseOrdersAsync();

        public Task<PurchaseOrder> GetPurchaseOrderByIdAsync(string id);

        public PurchaseOrder GetPurchaseOrderById(string id);

        public Task UpdatePurchaseOrderAsync(string id, PurchaseOrder purchaseOrder);
    }
}
