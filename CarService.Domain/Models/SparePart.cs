using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class SparePart
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимум 6, максимум 20 символов!")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public Model Model { get; set; }
        public int Count { get; set; }
    }
}
