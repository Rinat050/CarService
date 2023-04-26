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

        public Task<BaseResponse<List<PurchaseOrderTableItem>>> GetPurchaseOrdersByDateAsync(DateTime from, DateTime to);

        public Task<BaseResponse<List<PurchaseOrderTableItem>>> GetPurchaseOrdersByDiagnosticianIdAsync(string diagnosticianId);

        public Task<BaseResponse<List<PurchaseOrderTableItem>>> GetPurchaseOrdersByMechanicIdAsync(string mechanicId);

        public Task<BaseResponse<PurchaseOrder>> GetPurchaseOrderByIdAsync(string id);
    }
}
