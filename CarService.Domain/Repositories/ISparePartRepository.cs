using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface ISparePartRepository
    {
        public Task AddSparePartAsync(SparePart sparePart);

        public Task<List<SparePart>> GetAllSparePartsAsync();

        public Task<SparePart> GetSparePartByNumberAsync(string number);

        public Task<SparePart> GetSparePartByIdAsync(string id);

        public Task<List<SparePart>> GetSparePartsByModelAsync(Model model);

        public SparePart GetSparePartById(string id);

        public Task UpdateSparePartAsync(string id, SparePart sparePart);

        public Task UpdateSparePartCountAsync(string id, int count);
    }
}
