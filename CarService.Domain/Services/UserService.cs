using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IPurchaseOrderRepository _purchaseOrderRepository;

        public User CurrentUser { get; set; }

        public UserService(IUserRepository userRepository, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _userRepository = userRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
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

        public async Task<BaseResponse<List<User>>> GetUsersByRoleAsync(Roles role)
        {
            try
            {
                var users = await _userRepository.GetUsersByRoleAsync(role);

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

        public async Task<BaseResponse<List<User>>> GetAvailableUsersByRoleAsync(Roles role)
        {
            try
            {
                var users = await _userRepository.GetUsersByRoleAsync(role);

                List<User> result = new List<User>();

                for(int i = 0; i < users.Count; ++i)
                {
                    if(IsUserAvailable(users[i]))
                    {
                        result.Add(users[i]);
                    }
                }

                return new BaseResponse<List<User>>()
                {
                    Success = true,
                    Data = result
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

        private bool IsUserAvailable(User user)
        {
            switch(user.Role)
            {
                case Enums.Roles.Diagnostician:
                    return !_purchaseOrderRepository
                        .IsPurchaseOrderExistByStatusAndDiagnostician(Enums.OrderStatus.Created, user.Id);
                case Enums.Roles.Mechanic:
                    return !(_purchaseOrderRepository
                        .IsPurchaseOrderExistByStatusAndMechanic(Enums.OrderStatus.Created, user.Id) ||
                        _purchaseOrderRepository
                        .IsPurchaseOrderExistByStatusAndMechanic(Enums.OrderStatus.Diagnosed, user.Id) ||
                        _purchaseOrderRepository
                        .IsPurchaseOrderExistByStatusAndMechanic(Enums.OrderStatus.InWork, user.Id));
                default:
                    return false;
            }
        }
    }
}
