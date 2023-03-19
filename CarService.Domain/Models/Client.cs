using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Client
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Необходимо 11 символов!")]
        public string PhoneNumber { get; set; }
    }
}
