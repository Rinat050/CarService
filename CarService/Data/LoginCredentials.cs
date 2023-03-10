using System.ComponentModel.DataAnnotations;

namespace CarService.Data
{
    public class LoginCredentials
    {
        [Required(ErrorMessage = "Поле не может быть пустым!")]

        public string Login { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимум 6, максимум 20 символов!")]
        public string Password { get; set; }
    }
}
