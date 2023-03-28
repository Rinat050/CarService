using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IUserService
    {
        public User CurrentUser { get; set; }

        public Task<BaseResponse<User>> CreateUserAsync(User user);

        public Task<BaseResponse<User>> UpdateUserAsync(User user);

        public Task<BaseResponse<User>> GetUserByLoginAndPasswordAsync(string login, string password);

        public Task<BaseResponse<User>> GetUserByLoginAsync(string login);

        public Task<BaseResponse<List<User>>> GetAllUsersAsync();

        public Task<BaseResponse<List<User>>> GetAvailableUsersByRoleAsync(Roles role);

        public Task<BaseResponse<List<User>>> GetUsersByRoleAsync(Roles role);
    }
}
