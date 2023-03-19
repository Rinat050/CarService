using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;

        public ClientService(IClientRepository repository)
        {
            _clientRepository = repository;
        }

        public async Task<BaseResponse<Client>> CreateClientAsync(Client client)
        {
            try
            {
                var clientDb = await _clientRepository.GetClientByPhoneNumberAsync(client.PhoneNumber);

                if (clientDb is not null)
                {
                    return new BaseResponse<Client>()
                    {
                        Success = false,
                        Description = "Клиент с таким номером телефона уже существует!"
                    };
                }

                await _clientRepository.AddClientAsync(client);

                return new BaseResponse<Client>()
                {
                    Success = true,
                    Description = "Клиент успешно добавлен!"
                };
            }
            catch
            {
                return new BaseResponse<Client>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<List<Client>>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllClientsAsync();

                return new BaseResponse<List<Client>>()
                {
                    Success = true,
                    Data = clients
                };
            }
            catch
            {
                return new BaseResponse<List<Client>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<Client>> UpdateClientAsync(Client client)
        {
            try
            {
                var clientDb = await _clientRepository.GetClientByIdAsync(client.Id);

                if (clientDb is null)
                {
                    return new BaseResponse<Client>()
                    {
                        Success = false,
                        Description = "Клиент не найден!"
                    };
                }

                clientDb = await _clientRepository.GetClientByPhoneNumberAsync(client.PhoneNumber);

                if (clientDb is not null && clientDb.Id != client.Id)
                {
                    return new BaseResponse<Client>()
                    {
                        Success = false,
                        Description = "Клиент с таким номером телефона уже существует!"
                    };
                }

                await _clientRepository.UpdateClientAsync(client.Id, client);

                return new BaseResponse<Client>()
                {
                    Success = true,
                    Description = "Данные успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<Client>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
