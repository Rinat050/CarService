using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository repository)
        {
            _supplierRepository = repository;
        }

        public async Task<BaseResponse<Supplier>> CreateSupplierAsync(Supplier supplier)
        {
            try
            {
                var supplierDb = await _supplierRepository.GetSupplierByInnAsync(supplier.Inn);

                if (supplierDb is not null)
                {
                    return new BaseResponse<Supplier>()
                    {
                        Success = false,
                        Description = "Поставщик с таким ИНН уже существует!"
                    };
                }

                await _supplierRepository.AddSupplierAsync(supplier);

                return new BaseResponse<Supplier>()
                {
                    Success = true,
                    Description = "Поставщик успешно добавлен!"
                };
            }
            catch
            {
                return new BaseResponse<Supplier>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<SupplierTableItem>>> GetAllSuppliersAsync()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllSuppliersAsync();

                return new BaseResponse<List<SupplierTableItem>>()
                {
                    Success = true,
                    Data = suppliers
                };
            }
            catch
            {
                return new BaseResponse<List<SupplierTableItem>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Supplier>> GetSupplierByIdAsync(string id)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(id);

                if (supplier is null)
                {
                    return new BaseResponse<Supplier>()
                    {
                        Success = false,
                        Description = "Поставщик не найден!"
                    };
                }

                return new BaseResponse<Supplier>()
                {
                    Success = true,
                    Data = supplier
                };
            }
            catch
            {
                return new BaseResponse<Supplier>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<SupplierSparePartItem>>> GetSupplierSparePartsAsync(string supplierId)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId);

                if (supplier is null)
                {
                    return new BaseResponse<List<SupplierSparePartItem>> ()
                    {
                        Success = false,
                        Description = "Поставщик не найден!"
                    };
                }

                var sparePartsList = await _supplierRepository.GetSupplierSparePartsAsync(supplierId);

                return new BaseResponse<List<SupplierSparePartItem>> ()
                {
                    Success = true,
                    Data = sparePartsList
                };
            }
            catch
            {
                return new BaseResponse<List<SupplierSparePartItem>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Supplier>> UpdateSupplierAsync(Supplier supplier)
        {
            try
            {
                var supplierDb = await _supplierRepository.GetSupplierByIdAsync(supplier.Id);

                if (supplierDb is null)
                {
                    return new BaseResponse<Supplier>()
                    {
                        Success = false,
                        Description = "Поставщик не найден!"
                    };
                }

                supplierDb = await _supplierRepository.GetSupplierByInnAsync(supplier.Inn);

                if (supplierDb is not null && supplierDb.Id != supplier.Id)
                {
                    return new BaseResponse<Supplier>()
                    {
                        Success = false,
                        Description = "Поставщик с таким ИНН уже существует!"
                    };
                }

                await _supplierRepository.UpdateSupplierAsync(supplier.Id, supplier);

                return new BaseResponse<Supplier>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Supplier>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
