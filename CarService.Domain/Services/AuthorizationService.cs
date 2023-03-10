using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CarService.Domain.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private IUserService _userService;
        
        public AuthorizationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse<User>> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            try
            {
                var result = await _userService.GetUserByLoginAsync(user.Login);

                if (!result.Success)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = result.Description
                    };
                }

                result = await _userService.GetUserByLoginAndPasswordAsync(user.Login, HashPassword(oldPassword));

                if (!result.Success)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = "Неверный пароль!"
                    };
                }

                user.Password = HashPassword(newPassword);

                await _userService.UpdateUserAsync(user);

                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Пароль успешно обновлен"
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

        public async Task<BaseResponse<User>> LoginAsync(string login, string password)
        {
            try
            {
                var result = await _userService.GetUserByLoginAndPasswordAsync(login, HashPassword(password));

                if (!result.Success)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = result.Description,
                    };
                }

                _userService.CurrentUser = result.Data!;

                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Успешная авторизация!"
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

        public async Task<BaseResponse<User>> LogoutAsync()
        {
            try
            {
                _userService.CurrentUser = null;

                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Успешно!"
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

        public async Task<BaseResponse<User>> RegisterAsync(string login, string password, string name, string surname, 
                                                            string patronymic, string address, string phone, Roles role)
        {
            try
            {
                var result = await _userService.GetAllUsersAsync();

                if (!result.Success)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = result.Description
                    };
                }

                var userDb = result.Data?.FirstOrDefault(x => x.Login == login);

                if (userDb is not null)
                {
                    return new BaseResponse<User>()
                    {
                        Success = false,
                        Description = "Пользователь с таким логином уже существует!"
                    };
                }

                var user = new User()
                {
                    Login = login,
                    Password = password,
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    Address = address,
                    PhoneNumber = phone,
                    Role = role
                };

                user.Password = HashPassword(user.Password);

                await _userService.CreateUserAsync(user);

                return new BaseResponse<User>()
                {
                    Success = true,
                    Description = "Успешная регистрация!"
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

        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
