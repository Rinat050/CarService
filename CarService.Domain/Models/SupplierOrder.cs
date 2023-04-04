namespace CarService.Domain.Models
{
    public class SupplierOrder
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Supplier Supplier { get; set; }
        public List<SparePartListItem> SpareParts { get; set; }
    }
}
