using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IRepairService
    {
        public Task<BaseResponse<Repair>> CreateRepairAsync(Repair repair);

        public Task<BaseResponse<Repair>> UpdateRepairAsync(Repair repair);

        public Task<BaseResponse<List<Repair>>> GetAllRepairsAsync();
    }
}
