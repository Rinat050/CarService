using CarService.Domain.Enums;

namespace CarService.Domain.Models
{
    public class PurchaseOrder
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public Client Client { get; set; }
        public Car Car { get; set; }
        public User Manager { get; set; }
        public User Diagnostician { get; set; }
        public User Mechanic { get; set; }
        public List<Defect>? Defects { get; set; }
        public List<SparePartListItem>? SpareParts { get; set; }
        public List<RepairListItem>? CompletedWorks { get; set; }
    }
}
