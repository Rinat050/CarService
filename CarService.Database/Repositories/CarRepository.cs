using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class CarRepository : ICarRepository
    {
        private IMongoCollection<CarDb> _cars;
        private IClientRepository _clientRepository;
        private IModelRepository _modelRepository;

        public CarRepository(MongoConnection connection, 
            IModelRepository modelRepository, IClientRepository clientRepository)
        {
            _cars = connection.Database.GetCollection<CarDb>("Cars");
            _clientRepository = clientRepository;
            _modelRepository = modelRepository;
        }
        public async Task AddCarAsync(Car car)
        {
            var carDb = new CarDb()
            {
                ModelId = car.Model.Id,
                StateNumber = car.StateNumber,
                VinNumber = car.VinNumber,
                ClientID = car.Client.Id
            };

            await _cars.InsertOneAsync(carDb);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await _cars
                .FindAsync<CarDb>(x => true);

            return cars
                .ToEnumerable()
                .Select(x => new Car()
                {
                    Id = x.Id.ToString(),
                    VinNumber = x.VinNumber,
                    StateNumber = x.StateNumber,
                    Client = _clientRepository.GetClientById(x.ClientID),
                    Model = _modelRepository.GetModelById(x.ModelId)
                }).ToList<Car>();
        }

        public Car GetCarById(string id)
        {
            var filter = Builders<CarDb>.Filter.Eq("_id", new ObjectId(id));

            var cars = _cars
                .Find<CarDb>(filter);

            var carInfo = cars.FirstOrDefault();

            if (carInfo is not null)
            {
                return new Car()
                {
                    Id = carInfo.Id.ToString(),
                    VinNumber = carInfo.VinNumber,
                    StateNumber = carInfo.StateNumber,
                    Model = _modelRepository.GetModelById(carInfo.ModelId),
                    Client = _clientRepository.GetClientById(carInfo.ClientID),
                };
            }

            return null;
        }

        public async Task<Car> GetCarByIdAsync(string id)
        {
            var filter = Builders<CarDb>.Filter.Eq("_id", new ObjectId(id));

            var cars = await _cars
                .FindAsync<CarDb>(filter);

            var carInfo = cars.FirstOrDefault();

            if (carInfo is not null)
            {
                return new Car()
                {
                    Id = carInfo.Id.ToString(),
                    VinNumber = carInfo.VinNumber,
                    StateNumber = carInfo.StateNumber,
                    Model = _modelRepository.GetModelById(carInfo.ModelId),
                    Client = _clientRepository.GetClientById(carInfo.ClientID),
                };
            }

            return null;
        }

        public async Task<Car> GetCarByVinNumberAsync(string vinNumber)
        {
            var cars = await _cars
                .FindAsync<CarDb>(x => x.VinNumber == vinNumber);

            var carInfo = cars.FirstOrDefault();

            if (carInfo is not null)
            {
                return new Car()
                {
                    Id = carInfo.Id.ToString(),
                    VinNumber = carInfo.VinNumber,
                    StateNumber = carInfo.StateNumber,
                    Model = _modelRepository.GetModelById(carInfo.ModelId),
                    Client = _clientRepository.GetClientById(carInfo.ClientID),
                };
            }

            return null;
        }

        public async Task<List<Car>> GetCarsByClientAsync(Client client)
        {
            var cars = await _cars
                .FindAsync<CarDb>(x => x.ClientID == client.Id);

            return cars
                .ToEnumerable()
                .Select(x => new Car()
                {
                    Id = x.Id.ToString(),
                    VinNumber = x.VinNumber,
                    StateNumber = x.StateNumber,
                    Client = _clientRepository.GetClientById(x.ClientID),
                    Model = _modelRepository.GetModelById(x.ModelId)
                }).ToList<Car>();
        }

        public async Task UpdateCarAsync(string id, Car car)
        {
            var carDb = new CarDb()
            {
                Id = ObjectId.Parse(car.Id),
                ModelId = car.Model.Id,
                StateNumber = car.StateNumber,
                VinNumber = car.VinNumber,
                ClientID = car.Client.Id
            };

            var filter = Builders<CarDb>.Filter.Eq("_id", new ObjectId(id));

            await _cars.ReplaceOneAsync(filter, carDb);
        }
    }
}
