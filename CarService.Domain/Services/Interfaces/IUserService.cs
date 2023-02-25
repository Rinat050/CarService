using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponse<User>> CreateUserAsync(User user);

        public Task<BaseResponse<List<User>>> GetAllUsersAsync();
    }
}
