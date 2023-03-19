using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private IMongoCollection<ClientDb> _clients;

        public ClientRepository(MongoConnection connection)
        {
            _clients = connection.Database.GetCollection<ClientDb>("Clients");
        }

        public async Task AddClientAsync(Client client)
        {
            var clientDb = new ClientDb()
            {
                Name = client.Name,
                Surname = client.Surname,
                Patronymic = client.Patronymic,
                PhoneNumber = client.PhoneNumber,
            };

            await _clients.InsertOneAsync(clientDb);
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            var clients = await _clients
                .FindAsync<ClientDb>(x => true);

            return clients
                .ToEnumerable()
                .Select(x => new Client()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    PhoneNumber = x.PhoneNumber
                }).ToList<Client>();
        }

        public Client GetClientById(string id)
        {
            var filter = Builders<ClientDb>.Filter.Eq("_id", new ObjectId(id));

            var clients = _clients
                .Find<ClientDb>(filter);

            var clientInfo = clients.FirstOrDefault();

            if (clientInfo is not null)
            {
                return new Client()
                {
                    Id = clientInfo.Id.ToString(),
                    Name = clientInfo.Name,
                    Surname = clientInfo.Surname,
                    Patronymic = clientInfo.Patronymic,
                    PhoneNumber = clientInfo.PhoneNumber
                };
            }

            return null;
        }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            var filter = Builders<ClientDb>.Filter.Eq("_id", new ObjectId(id));

            var clients = await _clients
                .FindAsync<ClientDb>(filter);

            var clientInfo = clients.FirstOrDefault();

            if (clientInfo is not null)
            {
                return new Client()
                {
                    Id = clientInfo.Id.ToString(),
                    Name = clientInfo.Name,
                    Surname = clientInfo.Surname,
                    Patronymic = clientInfo.Patronymic,
                    PhoneNumber = clientInfo.PhoneNumber
                };
            }

            return null;
        }

        public async Task<Client> GetClientByPhoneNumberAsync(string phoneNumber)
        {
            var client = await _clients
              .FindAsync<ClientDb>(x => x.PhoneNumber == phoneNumber);

            var clientInfo = client.FirstOrDefault();

            if (clientInfo is not null)
            {
                return new Client()
                {
                    Id = clientInfo.Id.ToString(),
                    Name = clientInfo.Name,
                    Surname = clientInfo?.Surname,
                    PhoneNumber = clientInfo?.PhoneNumber,
                    Patronymic = clientInfo?.Patronymic,
                };
            }

            return null;
        }

        public async Task UpdateClientAsync(string id, Client client)
        {
            var clientDb = new ClientDb()
            {
                Id = ObjectId.Parse(client.Id), 
                Name = client.Name,
                Surname = client.Surname,
                Patronymic = client.Patronymic,
                PhoneNumber = client.PhoneNumber
            };

            var filter = Builders<ClientDb>.Filter.Eq("_id", new ObjectId(id));

            await _clients.ReplaceOneAsync(filter, clientDb);
        }
    }
}
