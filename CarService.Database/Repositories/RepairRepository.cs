using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class RepairRepository : IRepairRepository
    {
        private IMongoCollection<RepairDb> _repairs;

        public RepairRepository(MongoConnection connection)
        {
            _repairs = connection.Database.GetCollection<RepairDb>("Repairs");
        }

        public async Task AddRepairAsync(Repair repair)
        {
            var repairDb = new RepairDb()
            {
                Description = repair.Description,
                Cost = repair.Cost
            };

            await _repairs.InsertOneAsync(repairDb);
        }

        public async Task<List<Repair>> GetAllRepairsAsync()
        {
            var repairs = await _repairs
                .FindAsync<RepairDb>(x => true);

            return repairs
                .ToEnumerable()
                .Select(x => new Repair()
                {
                    Id = x.Id.ToString(),
                    Description = x.Description,
                    Cost = x.Cost
                }).ToList<Repair>();
        }

        public async Task<Repair> GetRepairByDescriptionAsync(string description)
        {
            var repair = await _repairs
                 .FindAsync<RepairDb>(x => x.Description == description);

            var repairInfo = repair.FirstOrDefault();

            if (repairInfo is not null)
            {
                return new Repair()
                {
                    Id = repairInfo.Id.ToString(),
                    Description = repairInfo.Description,
                    Cost = repairInfo.Cost
                };
            }

            return null;
        }

        public Repair GetRepairById(string id)
        {
            var filter = Builders<RepairDb>.Filter.Eq("_id", new ObjectId(id));

            var repairs = _repairs
                .Find<RepairDb>(filter);

            var repairInfo = repairs.FirstOrDefault();

            if (repairInfo is not null)
            {
                return new Repair()
                {
                    Id = repairInfo.Id.ToString(),
                    Description = repairInfo.Description,
                    Cost = repairInfo.Cost
                };
            }

            return null;
        }

        public async Task<Repair> GetRepairByIdAsync(string id)
        {
            var filter = Builders<RepairDb>.Filter.Eq("_id", new ObjectId(id));

            var repairs = await _repairs
                .FindAsync<RepairDb>(filter);

            var repairInfo = repairs.FirstOrDefault();

            if (repairInfo is not null)
            {
                return new Repair()
                {
                    Id = repairInfo.Id.ToString(),
                    Description = repairInfo.Description,
                    Cost = repairInfo.Cost
                };
            }

            return null;
        }

        public async Task UpdateRepairAsync(string id, Repair repair)
        {
            var repairDb = new RepairDb() { Id = ObjectId.Parse(repair.Id), Description = repair.Description };

            var filter = Builders<RepairDb>.Filter.Eq("_id", new ObjectId(id));

            await _repairs.ReplaceOneAsync(filter, repairDb);
        }
    }
}
