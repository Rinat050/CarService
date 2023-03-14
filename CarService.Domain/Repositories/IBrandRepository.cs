using CarService.Domain.Models;
using System;
namespace CarService.Domain.Repositories
{
    public interface IBrandRepository
    {
        public Task AddBrandAsync(Brand brand);

        public Task<List<Brand>> GetAllBrandsAsync();

        public Task<Brand> GetBrandByNameAsync(string name);

        public Task<Brand> GetBrandByIdAsync(string id);

        public Task UpdateBrandAsync(string id, Brand brand);
    }
}
