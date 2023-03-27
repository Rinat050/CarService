using CarService.Domain.Enums;

namespace CarService.Domain.Models
{
    public class PurchaseOrderTableItem
    {
        public string PurchaseOrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Client { get; set; }
        public string Car { get; set; }
        public int TotalCost { get; set; }
        public OrderStatus Status { get; set; } 
    }
}
