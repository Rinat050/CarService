using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface ISupplierService
    {
        public Task<BaseResponse<Supplier>> CreateSupplierAsync(Supplier supplier);

        public Task<BaseResponse<Supplier>> UpdateSupplierAsync(Supplier supplier);

        public Task<BaseResponse<List<SupplierTableItem>>> GetAllSuppliersAsync();

        public Task<BaseResponse<Supplier>> GetSupplierByIdAsync(string id);

        public Task<BaseResponse<List<SupplierSparePartItem>>> GetSupplierSparePartsAsync(string supplierId);
    }
}
