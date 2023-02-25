using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public Task<BaseResponse<User>> LoginAsync(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> RegisterAsync(string name, string surname, string patronymic, string address, string phone, Roles role)
        {
            throw new NotImplementedException();
        }
    }
}
