using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IDefectService
    {
        public Task<BaseResponse<Defect>> CreateDefectAsync(Defect defect);

        public Task<BaseResponse<Defect>> UpdateDefectAsync(Defect defect);

        public Task<BaseResponse<List<Defect>>> GetAllDefectsAsync();
    }
}
