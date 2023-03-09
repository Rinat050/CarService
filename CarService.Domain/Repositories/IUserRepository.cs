using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserByIdAsync(string id);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}
