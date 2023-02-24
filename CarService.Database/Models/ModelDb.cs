using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class ModelDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? BrandId { get; set; }
    }
}
