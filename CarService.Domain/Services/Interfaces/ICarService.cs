using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface ICarService
    {
        public Task<BaseResponse<Car>> CreateCarAsync(Car car);

        public Task<BaseResponse<Car>> UpdateCarAsync(Car car);

        public Task<BaseResponse<List<Car>>> GetAllCarsAsync();

        public Task<BaseResponse<List<Car>>> GetCarsByClientAsync(Client client);
    }
}
