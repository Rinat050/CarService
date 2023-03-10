using CarService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимум 6, максимум 20 символов!")]
        public string Login { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }

        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Roles Role { get; set; }
    }
}
