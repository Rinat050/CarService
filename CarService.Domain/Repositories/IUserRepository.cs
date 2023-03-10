using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task UpdateUserAsync(string id, User user);
        public Task<User?> GetUserByIdAsync(string id);
        public Task<User?> GetUserByLoginAsync(string login);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}
