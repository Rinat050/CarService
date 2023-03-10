using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public User CurrentUser { get; set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> CreateUserAsync(User user)
        {
            try
            {
                await _userRepository.AddUserAsync(user);

                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Пользователь успешно добавлен"
                };
            }
            catch
            {
                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<List<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();

                return new BaseResponse<List<User>>()
                {
                    Success = true,
                    Data = users
                };
            }
            catch
            {
                return new BaseResponse<List<User>>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<User>> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            try
            {
                var user = await _userRepository.GetUserByLoginAndPasswordAsync(login, password);

                if (user is not null)
                {
                    return new BaseResponse<User>()
                    {
                        Success = true,
                        Data = user
                    };
                }

                return new BaseResponse<User>()
                {
                    Success = false,
                    Description = "Пользователь с такими данными не найден!"
                };
            }
            catch
            {
                return new BaseResponse<User>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }

        public async Task<BaseResponse<User>> UpdateUserAsync(User user)
        {
            try
            {
                var userDb = await _userRepository.GetUserByIdAsync(user.Id);

                if (userDb is null)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = "Пользователь не найден!"
                    };
                }

                userDb = await _userRepository.GetUserByLoginAsync(user.Login);

                if (userDb is not null && userDb.Id != user.Id)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = "Пользователь с таким логином уже существует!"
                    };
                }

                await _userRepository.UpdateUserAsync(user.Id, user);

                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Данные пользователя успешно обновлены!"
                };
            }
            catch
            {
                return new BaseResponse<User>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<User>> GetUserByLoginAsync(string login)
        {
            try
            {
                var user = await _userRepository.GetUserByLoginAsync(login);

                if (user is not null)
                {
                    return new BaseResponse<User>()
                    {
                        Success = true,
                        Data = user
                    };
                }

                return new BaseResponse<User>()
                {
                    Success = false,
                    Description = "Пользователь с таким логином не найден!"
                };
            }
            catch
            {
                return new BaseResponse<User>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка"
                };
            }
        }
    }
}
