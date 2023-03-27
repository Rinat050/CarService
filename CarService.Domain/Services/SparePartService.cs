using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class SparePartService : ISparePartService
    {
        private ISparePartRepository _sparePartRepository;

        public SparePartService(ISparePartRepository repository)
        {
            _sparePartRepository = repository;
        }
        public async Task<BaseResponse<SparePart>> CreateSparePartAsync(SparePart sparePart)
        {
            try
            {
                var sparePartDb = await _sparePartRepository.GetSparePartByNumberAsync(sparePart.Number);

                if (sparePartDb is not null)
                {
                    return new BaseResponse<SparePart>()
                    {
                        Success = false,
                        Description = "Запчасть с таким артикулом уже существует!"
                    };
                }

                await _sparePartRepository.AddSparePartAsync(sparePart);

                return new BaseResponse<SparePart>()
                {
                    Success = true,
                    Description = "Запчасть успешно добавлена!"
                };
            }
            catch
            {
                return new BaseResponse<SparePart>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<SparePart>>> GetAllSparePartsAsync()
        {
            try
            {
                var spareParts = await _sparePartRepository.GetAllSparePartsAsync();

                return new BaseResponse<List<SparePart>>()
                {
                    Success = true,
                    Data = spareParts
                };
            }
            catch
            {
                return new BaseResponse<List<SparePart>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<SparePart>>> GetSparePartsByModelAsync(Model model)
        {
            try
            {
                var spareParts = await _sparePartRepository.GetSparePartsByModelAsync(model);

                return new BaseResponse<List<SparePart>>()
                {
                    Success = true,
                    Data = spareParts
                };
            }
            catch
            {
                return new BaseResponse<List<SparePart>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<SparePart>> UpdateSparePartAsync(SparePart sparePart)
        {
            try
            {
                var sparePartDb = await _sparePartRepository.GetSparePartByIdAsync(sparePart.Id);

                if (sparePartDb is null)
                {
                    return new BaseResponse<SparePart>()
                    {
                        Success = false,
                        Description = "Запчасть не найдена!"
                    };
                }

                sparePartDb = await _sparePartRepository.GetSparePartByNumberAsync(sparePart.Number);

                if (sparePartDb is not null && sparePartDb.Id != sparePart.Id)
                {
                    return new BaseResponse<SparePart>()
                    {
                        Success = false,
                        Description = "Запчасть с таким артикулом уже существует!"
                    };
                }

                await _sparePartRepository.UpdateSparePartAsync(sparePart.Id, sparePart);

                return new BaseResponse<SparePart>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<SparePart>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<SparePart>> ChangeSparePartCountAsync(string sparePartId, int count)
        {
            try
            {
                var sparePartDb = await _sparePartRepository.GetSparePartByIdAsync(sparePartId);

                if (sparePartDb is null)
                {
                    return new BaseResponse<SparePart>()
                    {
                        Success = false,
                        Description = "Запчасть не найдена!"
                    };
                }

                if (sparePartDb.Count + count < 0)
                {
                    return new BaseResponse<SparePart>()
                    {
                        Success = false,
                        Description = "Недостаточно запчасти на складе!"
                    };
                }

                await _sparePartRepository.UpdateSparePartCountAsync(sparePartId, sparePartDb.Count + count);

                return new BaseResponse<SparePart>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<SparePart>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
