using CarService.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CarService.Data
{
    public class CreatePurchaseOrderCredentials
    {
        [Required(ErrorMessage = "Выберите клиента!")]
        public Client Client { get; set; }

        [Required(ErrorMessage = "Выберите автомобиль!")]
        public Car Car { get; set; }

        [Required(ErrorMessage = "Выберите диагноста!")]
        public User Diagnostician { get; set; }

        [Required(ErrorMessage = "Выберите механика!")]
        public User Mechanic { get; set; }
    }
}
