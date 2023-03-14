using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IBrandService
    {
        public Task<BaseResponse<Brand>> CreateBrandAsync(Brand user);

        public Task<BaseResponse<Brand>> UpdateBrandAsync(Brand user);

        public Task<BaseResponse<List<Brand>>> GetAllBrandsAsync();
    }
}
