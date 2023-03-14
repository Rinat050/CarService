using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class ModelService : IModelService
    {
        private IModelRepository _modelRepository;

        public ModelService(IModelRepository repository)
        {
            _modelRepository = repository;
        }

        public async Task<BaseResponse<Model>> CreateModelAsync(Model model)
        {
            try
            {
                var modelDb = await _modelRepository.GetModelByNameAsync(model.Name);

                if (modelDb is not null)
                {
                    return new BaseResponse<Model>()
                    {
                        Success = false,
                        Description = "Модель с таким названием уже существует!"
                    };
                }

                await _modelRepository.AddModelAsync(model);

                return new BaseResponse<Model>()
                {
                    Success = true,
                    Description = "Модель успешно добавлена!"
                };
            }
            catch
            {
                return new BaseResponse<Model>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<Model>>> GetAllModelsAsync()
        {
            try
            {
                var models = await _modelRepository.GetAllModelsAsync();

                return new BaseResponse<List<Model>>()
                {
                    Success = true,
                    Data = models
                };
            }
            catch
            {
                return new BaseResponse<List<Model>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Model>> UpdateModelAsync(Model model)
        {
            try
            {
                var modelDb = await _modelRepository.GetModelByIdAsync(model.Id);

                if (modelDb is null)
                {
                    return new BaseResponse<Model>()
                    {
                        Success = false,
                        Description = "Модель не найдена!"
                    };
                }

                modelDb = await _modelRepository.GetModelByNameAsync(model.Name);

                if (modelDb is not null && modelDb.Id != modelDb.Id)
                {
                    return new BaseResponse<Model>()
                    {
                        Success = false,
                        Description = "Модель с таким названием уже существует!"
                    };
                }

                await _modelRepository.UpdateModelAsync(model.Id, model);

                return new BaseResponse<Model>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Model>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
