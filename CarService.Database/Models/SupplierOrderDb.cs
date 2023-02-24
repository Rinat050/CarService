using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class SupplierOrderDb
    {
        [BsonId]
        public int Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SupplierId { get; set; }
        public Dictionary<int, int>? SpareParts { get; set; }
    }
}
