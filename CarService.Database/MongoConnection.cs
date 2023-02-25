using MongoDB.Driver;

namespace CarService.Database
{
    public static class MongoConnection
    {
        private static readonly MongoClient Client = new MongoClient("mongodb://localhost");
        public static readonly IMongoDatabase Database = Client.GetDatabase("CarService");
    }
}
