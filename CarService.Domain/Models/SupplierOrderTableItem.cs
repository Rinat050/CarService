namespace CarService.Domain.Models
{
    public class SupplierOrderTableItem
    {
        public string Id { get; set; }
        public string Supplier { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalCost { get; set; }
    }
}
