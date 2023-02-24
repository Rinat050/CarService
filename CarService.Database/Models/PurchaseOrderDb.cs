using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class PurchaseOrderDb
    {
        [BsonId]
        public int Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public int CarId { get; set; }
        public int ManagerId { get; set; }
        public int DiagnosticianId { get; set; }
        public int MechanicId { get; set; }
        public List<int>? Defects { get; set; }
        public Dictionary<int, int>? SpareParts { get; set; }
        public Dictionary<int, int>? CompletedWorks { get; set; }
    }
}
