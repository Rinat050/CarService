using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IModelRepository
    {
        public Task AddModelAsync(Model model);

        public Task<List<Model>> GetAllModelsAsync();

        public Task<Model> GetModelByNameAsync(string name);

        public Task<Model> GetModelByIdAsync(string id);

        public Task UpdateModelAsync(string id, Model model);
    }
}
