using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<BaseResponse<User>> RegisterAsync(string login, string password, string name, string surname,
                                                     string patronymic, string address, string phone, Roles role);
        public Task<BaseResponse<User>> LoginAsync(string login, string password);
    }
}
