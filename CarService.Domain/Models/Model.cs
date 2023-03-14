using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Model
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите бренд!")]
        public Brand Brand { get; set; }
    }
}
