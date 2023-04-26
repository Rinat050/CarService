namespace CarService.Domain.Models
{
    public class PurchaseOrderReportItem
    {
        public string PurchaseOrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalCost { get; set; }
    }
}
