using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private ISupplierOrderRepository _orderRepository;
        private ISparePartService _sparePartService;

        public SupplierOrderService(ISupplierOrderRepository repository, ISparePartService sparePartService)
        {
            _orderRepository = repository;
            _sparePartService = sparePartService;
        }

        public async Task<BaseResponse<SupplierOrder>> CreateSupplierOrderAsync(SupplierOrder order)
        {
            try
            {
                await _orderRepository.AddSupplierOrderAsync(order);

                var res = await ChangeSparePartsCountAsync(order.SpareParts);

                if (!res)
                {
                    return new BaseResponse<SupplierOrder>()
                    {
                        Success = false,
                        Description = "Произошла ошибка!"
                    };
                }

                return new BaseResponse<SupplierOrder>()
                {
                    Success = true,
                    Description = "Заказ успешно создан!"
                };
            }
            catch
            {
                return new BaseResponse<SupplierOrder>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<SupplierOrderTableItem>>> GetAllSuppliersOrdersAsync()
        {
            try
            {
                var suppliers = await _orderRepository.GetAllSuppliersOrdersAsync();

                return new BaseResponse<List<SupplierOrderTableItem>>()
                {
                    Success = true,
                    Data = suppliers
                };
            }
            catch
            {
                return new BaseResponse<List<SupplierOrderTableItem>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<SupplierOrder>> GetSupplierOrderByIdAsync(string id)
        {
            try
            {
                var order = await _orderRepository.GetSupplierOrderByIdAsync(id);

                if (order is null)
                {
                    return new BaseResponse<SupplierOrder>()
                    {
                        Success = false,
                        Description = "Заказ не найден!"
                    };
                }

                return new BaseResponse<SupplierOrder>()
                {
                    Success = true,
                    Data = order
                };
            }
            catch
            {
                return new BaseResponse<SupplierOrder>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<SupplierOrderTableItem>>> GetSuppliersOrdersByDateAsync(DateTime from, DateTime to)
        {
            try
            {
                var suppliers = await _orderRepository.GetSuppliersOrdersByDateAsync(from, to);

                return new BaseResponse<List<SupplierOrderTableItem>>()
                {
                    Success = true,
                    Data = suppliers
                };
            }
            catch
            {
                return new BaseResponse<List<SupplierOrderTableItem>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        private async Task<bool> ChangeSparePartsCountAsync(List<SparePartListItem> spareParts)
        {
            if (spareParts != null)
            {
                foreach (var part in spareParts)
                {
                    var res = await _sparePartService.ChangeSparePartCountAsync(part.SparePart.Id, part.Count);
                    if (!res.Success) return false;
                }
            }

            return true;
        }
    }
}
