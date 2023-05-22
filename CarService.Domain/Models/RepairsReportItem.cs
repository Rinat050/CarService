namespace CarService.Domain.Models
{
    public class RepairsReportItem
    {
        public Repair Repair { get; set; }
        public int Count { get; set; }
        public int TotalCost { get; set; }
    }
}
