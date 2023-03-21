using CarService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class PurchaseOrderDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public string ClientId { get; set; }
        public string CarId { get; set; }
        public string ManagerId { get; set; }
        public string DiagnosticianId { get; set; }
        public string MechanicId { get; set; }
        public List<string>? Defects { get; set; }
        public List<SparePartListItemDb>? SpareParts { get; set; }
        public List<RepairListItemDb>? CompletedWorks { get; set; }
    }
}
