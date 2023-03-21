using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Car
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Выберите модель!")]
        public Model Model { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(12, MinimumLength = 11, ErrorMessage = "Минимум 8 символов!")]
        public string StateNumber { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Необходимо 17 символов!")]
        public string VinNumber { get; set; }

        [Required(ErrorMessage = "Выберите владельца!")]
        public Client Client { get; set; }
    }
}
