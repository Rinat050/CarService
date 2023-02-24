using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class DefectDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string? Description { get; set; }
    }
}