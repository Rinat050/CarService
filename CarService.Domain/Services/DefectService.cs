using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class DefectService : IDefectService
    {
        private IDefectRepository _defectRepository;

        public DefectService(IDefectRepository repository)
        {
            _defectRepository = repository;
        }

        public async Task<BaseResponse<Defect>> CreateDefectAsync(Defect defect)
        {
            try
            {
                var defectDb = await _defectRepository.GetDefectByDescriptionAsync(defect.Description);

                if (defectDb is not null)
                {
                    return new BaseResponse<Defect>()
                    {
                        Success = false,
                        Description = "Неисправность с таким описанием уже существует!"
                    };
                }

                await _defectRepository.AddDefectAsync(defect);

                return new BaseResponse<Defect>()
                {
                    Success = true,
                    Description = "Неисправность успешно добавлена!"
                };
            }
            catch
            {
                return new BaseResponse<Defect>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<Defect>>> GetAllDefectsAsync()
        {
            try
            {
                var defects = await _defectRepository.GetAllDefectssAsync();

                return new BaseResponse<List<Defect>>()
                {
                    Success = true,
                    Data = defects
                };
            }
            catch
            {
                return new BaseResponse<List<Defect>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Defect>> UpdateDefectAsync(Defect defect)
        {
            try
            {
                var defectDb = await _defectRepository.GetDefectByIdAsync(defect.Id);

                if (defectDb is null)
                {
                    return new BaseResponse<Defect>()
                    {
                        Success = false,
                        Description = "Неисправность не найдена!"
                    };
                }

                defectDb = await _defectRepository.GetDefectByDescriptionAsync(defect.Description);

                if (defectDb is not null && defectDb.Id != defect.Id)
                {
                    return new BaseResponse<Defect>()
                    {
                        Success = false,
                        Description = "Неисправность с таким описанием уже существует!"
                    };
                }

                await _defectRepository.UpdateDefectAsync(defect.Id, defect);

                return new BaseResponse<Defect>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Defect>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
