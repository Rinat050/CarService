using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IClientRepository
    {
        public Task AddClientAsync(Client client);

        public Task<List<Client>> GetAllClientsAsync();

        public Task<Client> GetClientByIdAsync(string id);

        public Task<Client> GetClientByPhoneNumberAsync(string phoneNumber);

        public Client GetClientById(string id);

        public Task UpdateClientAsync(string id, Client client);
    }
}
