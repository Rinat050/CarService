using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Defect
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Description { get; set; }
    }
}
