using System.ComponentModel.DataAnnotations;

namespace CarService.Data
{
    public class AdminChangePasswordCredentials
    {

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимум 6, максимум 20 символов!")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают!")]
        public string RepeatPassword { get; set; }
    }
}
