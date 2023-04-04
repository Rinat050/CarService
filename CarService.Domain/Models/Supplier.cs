using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class Supplier
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string? Inn { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string? PhoneNumber { get; set; }

        public List<SupplierSparePartItem>? SpareParts { get; set; }
    }
}
