using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Car
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Выберите модель!")]
        public Model Model { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [RegularExpression(@"^[АВЕКМНОРСТУХ] \d{3} [АВЕКМНОРСТУХ]{2} \d{2,3}$", ErrorMessage = "Формат номера: А 777 АА 716!")]
        public string StateNumber { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [RegularExpression(@"^[0123456789ABCDEFGHJKLMNPRSTUVWXYZ]{17}$", ErrorMessage = "Недопустимый формат VIN - номера!")]
        public string VinNumber { get; set; }

        [Required(ErrorMessage = "Выберите владельца!")]
        public Client Client { get; set; }
    }
}
