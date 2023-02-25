using MongoDB.Driver;

namespace CarService.Database
{
    public class MongoConnection
    {
        private MongoClient _client;
        public IMongoDatabase Database;

        public MongoConnection(string client, string database)
        {
            _client = new MongoClient(client);
            Database = _client.GetDatabase(database);
        }
    }
}
