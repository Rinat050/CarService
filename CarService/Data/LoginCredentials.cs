using System.ComponentModel.DataAnnotations;

namespace CarService.Data
{
    public class LoginCredentials
    {
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Password { get; set; }
    }
}
