using CarService.Database.Models;
using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private IMongoCollection<UserDb> _users; 

        public UserRepository()
        {
            _users = MongoConnection.Database.GetCollection<UserDb>("Users");
        }

        public async Task AddUserAsync(User user)
        {
            var userDb = new UserDb()
            {
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Role = (int)user.Role
            };

            await _users.InsertOneAsync(userDb);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _users
                .FindAsync<UserDb>(x => true);

            return users
                .ToEnumerable()
                .Select(x => new User()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Role = (Roles)x.Role
                }).ToList<User>();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _users
                .FindAsync<UserDb>(x => x.Id.ToString() == id);

            var userInfo = user.FirstOrDefault();

            return new User()
            {
                Id = userInfo.Id.ToString(),
                Name = userInfo.Name,
                Surname = userInfo.Surname,
                Patronymic = userInfo.Patronymic,
                Address = userInfo.Address,
                PhoneNumber = userInfo.PhoneNumber,
                Role = (Roles) userInfo.Role
            };
        }
    }
}
