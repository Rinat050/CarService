using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IDefectRepository
    {
        public Task AddDefectAsync(Defect defect);

        public Task<List<Defect>> GetAllDefectssAsync();

        public Task<Defect> GetDefectByDescriptionAsync(string description);

        public Task<Defect> GetDefectByIdAsync(string id);

        public Defect GetDefectById(string id);

        public Task UpdateDefectAsync(string id, Defect defect);
    }
}
