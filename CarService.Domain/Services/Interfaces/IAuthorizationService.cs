using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<BaseResponse<User>> RegisterAsync(string login, string password, 
                                                    string name, string surname, Roles role);
        public Task<BaseResponse<User>> LoginAsync(string login, string password);

        public Task<BaseResponse<User>> LogoutAsync();

        public Task<BaseResponse<User>> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        public Task<BaseResponse<User>> ChangePasswordAsync(User user, string newPassword);
    }
}
