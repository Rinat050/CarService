namespace CarService.Domain.Models
{
    public class Supplier
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Inn { get; set; }
        public string? PhoneNumber { get; set; }
        public List<SupplierSparePartItem>? SpareParts { get; set; }
    }
}
