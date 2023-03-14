using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Brand
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }
    }
}
