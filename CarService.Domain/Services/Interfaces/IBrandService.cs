using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IBrandService
    {
        public Task<BaseResponse<Brand>> CreateBrandAsync(Brand brand);

        public Task<BaseResponse<Brand>> UpdateBrandAsync(Brand brand);

        public Task<BaseResponse<List<Brand>>> GetAllBrandsAsync();
    }
}
