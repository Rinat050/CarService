using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface ISupplierRepository
    {
        public Task AddSupplierAsync(Supplier supplier);

        public Task<List<SupplierTableItem>> GetAllSuppliersAsync();

        public Task<List<SupplierSparePartItem>> GetSupplierSparePartsAsync(string supplierId);

        public Task<Supplier> GetSupplierByInnAsync(string inn);

        public Task<Supplier> GetSupplierByIdAsync(string id);

        public string GetSupplierNameByIdAsync(string id);

        public Supplier GetSupplierById(string id);

        public Task UpdateSupplierAsync(string id, Supplier supplier);
    }
}
