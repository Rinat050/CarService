using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IRepairRepository
    {
        public Task AddRepairAsync(Repair repair);

        public Task<List<Repair>> GetAllRepairsAsync();

        public Task<Repair> GetRepairByDescriptionAsync(string description);

        public Task<Repair> GetRepairByIdAsync(string id);

        public Repair GetRepairById(string id);

        public Task UpdateRepairAsync(string id, Repair repair);
    }
}
