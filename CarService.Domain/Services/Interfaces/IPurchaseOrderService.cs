using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        public Task<BaseResponse<PurchaseOrder>> CreatePurchaseOrderAsync(Client client, Car car, 
                                                                User diagnostician, User mechanic);

        public Task<BaseResponse<PurchaseOrder>> UpdatePurchaseOrderAsync(PurchaseOrder order);

        public Task<BaseResponse<List<PurchaseOrderTableItem>>> GetAllPurchaseOrdersAsync();

        public Task<BaseResponse<PurchaseOrder>> GetPurchaseOrderByIdAsync(string id);
    }
}
