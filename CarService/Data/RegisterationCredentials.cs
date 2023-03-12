using CarService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarService.Data
{
    public class RegisterationCredentials
    {
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимум 6, максимум 20 символов!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимум 6, максимум 20 символов!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите роль!")]
        public Roles Role { get; set; }
    }
}
