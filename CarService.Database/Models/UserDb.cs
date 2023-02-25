using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarService.Database.Models
{
    public class UserDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int Role { get; set; }
    }
}