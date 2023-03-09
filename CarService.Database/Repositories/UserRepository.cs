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

        public UserRepository(MongoConnection connection)
        {
            _users = connection.Database.GetCollection<UserDb>("Users");
        }

        public async Task AddUserAsync(User user)
        {
            var userDb = new UserDb()
            {
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Role = (int)user.Role
            };

            await _users.InsertOneAsync(userDb);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _users
                .FindAsync<UserDb>(x => true);

            return users
                .ToEnumerable()
                .Select(x => new User()
                {
                    Id = x.Id.ToString(),
                    Login = x.Login,
                    Password = x.Password,
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Role = (Roles)x.Role
                }).ToList<User>();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var user = await _users
                .FindAsync<UserDb>(x => x.Id.ToString() == id);

            var userInfo = user.FirstOrDefault();

            if (userInfo is not null)
            {
                return new User()
                {
                    Id = userInfo.Id.ToString(),
                    Login = userInfo.Login,
                    Password = userInfo.Password,
                    Name = userInfo.Name,
                    Surname = userInfo.Surname,
                    Patronymic = userInfo.Patronymic,
                    Address = userInfo.Address,
                    PhoneNumber = userInfo.PhoneNumber,
                    Role = (Roles)userInfo.Role
                };
            }


            return null;
        }

        public async Task<User?> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            var user = await _users
                .FindAsync<UserDb>(x => x.Login == login && x.Password == password);

            var userInfo = user.FirstOrDefault();

            if (userInfo is not null)
            {
                return new User()
                {
                    Id = userInfo.Id.ToString(),
                    Login = userInfo.Login,
                    Password = userInfo.Password,
                    Name = userInfo.Name,
                    Surname = userInfo.Surname,
                    Patronymic = userInfo.Patronymic,
                    Address = userInfo.Address,
                    PhoneNumber = userInfo.PhoneNumber,
                    Role = (Roles)userInfo.Role
                };
            }

            return null;
        }
    }
}
