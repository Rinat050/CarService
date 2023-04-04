using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Models
{
    public class SupplierOrder
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Выберите поставщика!")]
        public SupplierTableItem Supplier { get; set; }
        public List<SparePartListItem> SpareParts { get; set; }
    }
}
