using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class BrandDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
    }
}
