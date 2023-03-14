using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class DefectRepository : IDefectRepository
    {
        private IMongoCollection<DefectDb> _defects;

        public DefectRepository(MongoConnection connection)
        {
            _defects = connection.Database.GetCollection<DefectDb>("Defects");
        }

        public async Task AddDefectAsync(Defect defect)
        {
            var defectDb = new DefectDb()
            {
                Description = defect.Description
            };

            await _defects.InsertOneAsync(defectDb);
        }

        public async Task<List<Defect>> GetAllDefectssAsync()
        {
            var defects = await _defects
                .FindAsync<DefectDb>(x => true);

            return defects
                .ToEnumerable()
                .Select(x => new Defect()
                {
                    Id = x.Id.ToString(),
                    Description = x.Description
                }).ToList<Defect>();
        }

        public Defect GetDefectById(string id)
        {
            var filter = Builders<DefectDb>.Filter.Eq("_id", new ObjectId(id));

            var defects = _defects
                .Find<DefectDb>(filter);

            var defectInfo = defects.FirstOrDefault();

            if (defectInfo is not null)
            {
                return new Defect()
                {
                    Id = defectInfo.Id.ToString(),
                    Description = defectInfo.Description,
                };
            }

            return null;
        }

        public async Task<Defect> GetDefectByIdAsync(string id)
        {
            var filter = Builders<DefectDb>.Filter.Eq("_id", new ObjectId(id));

            var defects = await _defects
                .FindAsync<DefectDb>(filter);

            var defectInfo = defects.FirstOrDefault();

            if (defectInfo is not null)
            {
                return new Defect()
                {
                    Id = defectInfo.Id.ToString(),
                    Description = defectInfo.Description,
                };
            }

            return null;
        }

        public async Task<Defect> GetDefectByDescriptionAsync(string description)
        {
            var defect = await _defects
                .FindAsync<DefectDb>(x => x.Description == description);

            var defectInfo = defect.FirstOrDefault();

            if (defectInfo is not null)
            {
                return new Defect()
                {
                    Id = defectInfo.Id.ToString(),
                    Description = defectInfo.Description,
                };
            }

            return null;
        }

        public async Task UpdateDefectAsync(string id, Defect defect)
        {
            var defectDb = new DefectDb() { Id = ObjectId.Parse(defect.Id), Description = defect.Description};

            var filter = Builders<DefectDb>.Filter.Eq("_id", new ObjectId(id));

            await _defects.ReplaceOneAsync(filter, defectDb);
        }
    }
}
