using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private ISupplierOrderRepository _orderRepository;

        public SupplierOrderService(ISupplierOrderRepository repository)
        {
            _orderRepository = repository;
        }

        public async Task<BaseResponse<SupplierOrder>> CreateSupplierOrderAsync(SupplierOrder order)
        {
            try
            {
                await _orderRepository.AddSupplierOrderAsync(order);

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
    }
}
