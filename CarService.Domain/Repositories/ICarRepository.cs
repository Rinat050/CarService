using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface ICarRepository
    {
        public Task AddCarAsync(Car car);

        public Task<List<Car>> GetAllCarsAsync();

        public Task<List<Car>> GetCarsByClientAsync(Client client);

        public Task<Car> GetCarByVinNumberAsync(string vinNumber);

        public Task<Car> GetCarByIdAsync(string id);

        public Car GetCarById(string id);

        public Task UpdateCarAsync(string id, Car car);
    }
}
