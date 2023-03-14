using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class RepairService : IRepairService
    {
        private IRepairRepository _repairRepository;

        public RepairService(IRepairRepository repository)
        {
            _repairRepository = repository;
        }

        public async Task<BaseResponse<Repair>> CreateRepairAsync(Repair repair)
        {
            try
            {
                var repairDb = await _repairRepository.GetRepairByDescriptionAsync(repair.Description);

                if (repairDb is not null)
                {
                    return new BaseResponse<Repair>()
                    {
                        Success = false,
                        Description = "Ремонтная работа с таким описанием уже существует!"
                    };
                }

                await _repairRepository.AddRepairAsync(repair);

                return new BaseResponse<Repair>()
                {
                    Success = true,
                    Description = "Ремонтная работа успешно добавлена!"
                };
            }
            catch
            {
                return new BaseResponse<Repair>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<Repair>>> GetAllRepairsAsync()
        {
            try
            {
                var repairs = await _repairRepository.GetAllRepairsAsync();

                return new BaseResponse<List<Repair>>()
                {
                    Success = true,
                    Data = repairs
                };
            }
            catch
            {
                return new BaseResponse<List<Repair>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Repair>> UpdateRepairAsync(Repair repair)
        {
            try
            {
                var repairDb = await _repairRepository.GetRepairByIdAsync(repair.Id);

                if (repairDb is null)
                {
                    return new BaseResponse<Repair>()
                    {
                        Success = false,
                        Description = "Ремонтная работа не найдена!"
                    };
                }

                repairDb = await _repairRepository.GetRepairByDescriptionAsync(repair.Description);

                if (repairDb is not null && repairDb.Id != repair.Id)
                {
                    return new BaseResponse<Repair>()
                    {
                        Success = false,
                        Description = "Ремонтная работа с таким описанием уже существует!"
                    };
                }

                await _repairRepository.UpdateRepairAsync(repair.Id, repair);

                return new BaseResponse<Repair>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Repair>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
