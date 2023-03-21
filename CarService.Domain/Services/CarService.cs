using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class CarService : ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository repository)
        {
            _carRepository = repository;
        }

        public async Task<BaseResponse<Car>> CreateCarAsync(Car car)
        {
            try
            {
                var carDb = await _carRepository.GetCarByVinNumberAsync(car.VinNumber);

                if (carDb is not null)
                {
                    return new BaseResponse<Car>()
                    {
                        Success = false,
                        Description = "Автомобиль с таким VIN - номером уже существует!"
                    };
                }

                await _carRepository.AddCarAsync(car);

                return new BaseResponse<Car>()
                {
                    Success = true,
                    Description = "Автомобиль успешно добавлен!"
                };
            }
            catch
            {
                return new BaseResponse<Car>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<Car>>> GetAllCarsAsync()
        {
            try
            {
                var cars = await _carRepository.GetAllCarsAsync();

                return new BaseResponse<List<Car>>()
                {
                    Success = true,
                    Data = cars
                };
            }
            catch
            {
                return new BaseResponse<List<Car>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<Car>>> GetCarsByClientAsync(Client client)
        {
            try
            {
                var cars = await _carRepository.GetCarsByClientAsync(client);

                return new BaseResponse<List<Car>>()
                {
                    Success = true,
                    Data = cars
                };
            }
            catch
            {
                return new BaseResponse<List<Car>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Car>> UpdateCarAsync(Car car)
        {
            try
            {
                var carDb = await _carRepository.GetCarByIdAsync(car.Id);

                if (carDb is null)
                {
                    return new BaseResponse<Car>()
                    {
                        Success = false,
                        Description = "Автомобиль не найден!"
                    };
                }

                carDb = await _carRepository.GetCarByVinNumberAsync(car.VinNumber);

                if (carDb is not null && carDb.Id != car.Id)
                {
                    return new BaseResponse<Car>()
                    {
                        Success = false,
                        Description = "Автомобиль с таким VIN - номером уже существует!"
                    };
                }

                await _carRepository.UpdateCarAsync(car.Id, car);

                return new BaseResponse<Car>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Car>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
