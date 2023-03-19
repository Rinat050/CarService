using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IClientService
    {
        public Task<BaseResponse<Client>> CreateClientAsync(Client client);

        public Task<BaseResponse<Client>> UpdateClientAsync(Client client);

        public Task<BaseResponse<List<Client>>> GetAllClientsAsync();
    }
}
