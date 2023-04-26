namespace CarService.Domain.Models
{
    public class CashFlowReport
    {
        public List<PurchaseOrderReportItem> PurchaseOrdersInfo { get; set; }
        public List<SupplierOrderReportItem> SupplierOrdersInfo { get; set; }
    }
}
