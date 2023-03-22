using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class CarDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string ModelId { get; set; } = null!;
        public string? StateNumber { get; set; }
        public string? VinNumber { get; set; }
        public string? ClientID { get; set; }
    }
}
