using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Repair
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Description { get; set; }

        [Range(100, 300000, ErrorMessage = "Минимум 100, максимум 300 тыс.")]
        public int Cost { get; set; }
    }
}
