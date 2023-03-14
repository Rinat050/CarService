using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IBrandRepository
    {
        public Task AddBrandAsync(Brand brand);

        public Task<List<Brand>> GetAllBrandsAsync();

        public Task<Brand> GetBrandByNameAsync(string name);

        public Task<Brand> GetBrandByIdAsync(string id);

        public Brand GetBrandById(string id);

        public Task UpdateBrandAsync(string id, Brand brand);
    }
}
