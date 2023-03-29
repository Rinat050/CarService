using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface ISparePartService
    {
        public Task<BaseResponse<SparePart>> CreateSparePartAsync(SparePart sparePart);

        public Task<BaseResponse<SparePart>> UpdateSparePartAsync(SparePart sparePart);

        public Task<BaseResponse<SparePart>> ChangeSparePartCountAsync(string sparePartId, int count);

        public Task<BaseResponse<List<SparePart>>> GetAllSparePartsAsync();

        public Task<BaseResponse<List<SparePart>>> GetAvailableSparePartsAsync();

        public Task<BaseResponse<List<SparePart>>> GetSparePartsByModelAsync(Model model);
    }
}
