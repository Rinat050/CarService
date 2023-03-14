using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;

        public BrandService(IBrandRepository repository)
        {
            _brandRepository = repository;
        }

        public async Task<BaseResponse<Brand>> CreateBrandAsync(Brand brand)
        {
            try
            {
                var brandDb = await _brandRepository.GetBrandByNameAsync(brand.Name);

                if (brandDb is not null)
                {
                    return new BaseResponse<Brand>()
                    {
                        Success = false,
                        Description = "Бренд с таким названием уже существует!"
                    };
                }

                await _brandRepository.AddBrandAsync(brand);

                return new BaseResponse<Brand>()
                {
                    Success = true,
                    Description = "Бренд успешно добавлен!"
                };
            }
            catch
            {
                return new BaseResponse<Brand>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<Brand>>> GetAllBrandsAsync()
        {
            try
            {
                var brands = await _brandRepository.GetAllBrandsAsync();

                return new BaseResponse<List<Brand>>()
                {
                    Success = true,
                    Data = brands
                };
            }
            catch
            {
                return new BaseResponse<List<Brand>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Brand>> UpdateBrandAsync(Brand brand)
        {
            try
            {
                var brandDb = await _brandRepository.GetBrandByIdAsync(brand.Id);

                if (brandDb is null)
                {
                    return new BaseResponse<Brand>()
                    {
                        Success = false,
                        Description = "Бренд не найден"
                    };
                }

                brandDb = await _brandRepository.GetBrandByNameAsync(brand.Name);

                if (brandDb is not null && brandDb.Id != brand.Id)
                {
                    return new BaseResponse<Brand>()
                    {
                        Success = false,
                        Description = "Бренд с таким названием уже существует!"
                    };
                }

                await _brandRepository.UpdateBrandAsync(brand.Id, brand);

                return new BaseResponse<Brand>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Brand>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
