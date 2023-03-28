using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository _orderRepository;
        private ISparePartService _sparePartService;
        private IUserService _userService;

        public PurchaseOrderService(IPurchaseOrderRepository orderRepository, 
            ISparePartService sparePartService, IUserService userService)
        {
            _orderRepository = orderRepository;
            _sparePartService = sparePartService;
            _userService = userService;
        }

        public async Task<BaseResponse<PurchaseOrder>> CreatePurchaseOrderAsync(Client client, Car car,
                                                                        User diagnostician, User mechanic)
        {
            try
            {
                await _orderRepository.AddPurchaseOrderAsync(new PurchaseOrder()
                {
                    Car = car,
                    Client = client,
                    Diagnostician = diagnostician,
                    Mechanic = mechanic,
                    Manager = _userService.CurrentUser,
                    CreatedDate = DateTime.Now,
                    Status = Enums.OrderStatus.Created
                });

                return new BaseResponse<PurchaseOrder>()
                {
                    Success = true,
                    Description = "Заказ-наряд успешно создан!"
                };
            }
            catch
            {
                return new BaseResponse<PurchaseOrder>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"

                };
            }
        }

        public async Task<BaseResponse<List<PurchaseOrderTableItem>>> GetAllPurchaseOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllPurchaseOrdersAsync();

                return new BaseResponse<List<PurchaseOrderTableItem>>()
                {
                    Success = true,
                    Data = orders
                };
            }
            catch
            {
                return new BaseResponse<List<PurchaseOrderTableItem>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            };
        }

        public async Task<BaseResponse<PurchaseOrder>> GetPurchaseOrderByIdAsync(string id)
        {
            try
            {
                var order = await _orderRepository.GetPurchaseOrderByIdAsync(id);

                if (order is null)
                {
                    return new BaseResponse<PurchaseOrder>()
                    {
                        Success = false,
                        Description = "Заказ-наряд не найден!"
                    };
                }

                return new BaseResponse<PurchaseOrder>()
                {
                    Success = true,
                    Data = order
                };
            }
            catch
            {
                return new BaseResponse<PurchaseOrder>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            };
        }     

        public async Task<BaseResponse<PurchaseOrder>> UpdatePurchaseOrderAsync(PurchaseOrder order)
        {
            try
            {
                var orderDb = await _orderRepository.GetPurchaseOrderByIdAsync(order.Id);

                if (orderDb is null)
                {
                    return new BaseResponse<PurchaseOrder>()
                    {
                        Success = false,
                        Description = "Заказ-наряд не найден!"
                    };
                }

                var res = await ChangeSparePartsCountAsync(orderDb.SpareParts, order.SpareParts);

                if (!res)
                {
                    return new BaseResponse<PurchaseOrder>()
                    {
                        Success = false,
                        Description = "Произошла ошибка!"
                    };
                }

                await _orderRepository.UpdatePurchaseOrderAsync(order.Id, order);

                return new BaseResponse<PurchaseOrder>()
                {
                    Success = true,
                    Description = "Данные успешно сохранены!"
                };         
            }
            catch
            {
                return new BaseResponse<PurchaseOrder>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        private async Task<bool> ChangeSparePartsCountAsync(List<SparePartListItem> oldSparePartsList, List<SparePartListItem> newSparePartsList)
        {
            foreach(var part in oldSparePartsList)
            {
                var res = await _sparePartService.ChangeSparePartCountAsync(part.SparePart.Id, part.Count);
                if (!res.Success) return false;
            }

            foreach(var part in newSparePartsList)
            {
                var res = await _sparePartService.ChangeSparePartCountAsync(part.SparePart.Id, -1 * part.Count);
                if (!res.Success) return false;
            }

            return true;
        }
    }
}
