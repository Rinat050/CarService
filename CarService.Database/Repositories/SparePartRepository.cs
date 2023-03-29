using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class SparePartRepository : ISparePartRepository
    {
        private IMongoCollection<SparePartDb> _spareParts;
        private IModelRepository _models;

        public SparePartRepository(MongoConnection connection, IModelRepository modelRepository)
        {
            _spareParts = connection.Database.GetCollection<SparePartDb>("SpareParts");
            _models = modelRepository;
        }

        public async Task AddSparePartAsync(SparePart sparePart)
        {
            var sparePartDb = new SparePartDb()
            {
                Name = sparePart.Name,
                Price = sparePart.Price,
                ModelId = sparePart.Model.Id,
                Count = sparePart.Count,
                Number = sparePart.Number,
            };

            await _spareParts.InsertOneAsync(sparePartDb);
        }

        public async Task<List<SparePart>> GetAllSparePartsAsync()
        {
            var spareParts = await _spareParts
                .FindAsync<SparePartDb>(x => true);

            return spareParts
                .ToEnumerable()
                .Select(x => new SparePart()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Count = x.Count,
                    Model = _models.GetModelById(x.ModelId),
                    Number = x.Number,
                }).ToList<SparePart>();
        }

        public async Task<List<SparePart>> GetAvailableSparePartsAsync()
        {
            var spareParts = await _spareParts
               .FindAsync<SparePartDb>(x => x.Count > 0);

            return spareParts
                .ToEnumerable()
                .Select(x => new SparePart()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Count = x.Count,
                    Model = _models.GetModelById(x.ModelId),
                    Number = x.Number,
                }).ToList<SparePart>();
        }

        public SparePart GetSparePartById(string id)
        {
            var filter = Builders<SparePartDb>.Filter.Eq("_id", new ObjectId(id));

            var spareParts = _spareParts
                .Find<SparePartDb>(filter);

            var sparePartInfo = spareParts.FirstOrDefault();

            if (sparePartInfo is not null)
            {
                return new SparePart()
                {
                    Id = sparePartInfo.Id.ToString(),
                    Name = sparePartInfo.Name,
                    Price = sparePartInfo.Price,
                    Model = _models.GetModelById(sparePartInfo.ModelId),
                    Count = sparePartInfo.Count,
                    Number = sparePartInfo.Number,
                };
            }

            return null;
        }

        public async Task<SparePart> GetSparePartByIdAsync(string id)
        {
            var filter = Builders<SparePartDb>.Filter.Eq("_id", new ObjectId(id));

            var spareParts = await _spareParts
                .FindAsync<SparePartDb>(filter);

            var sparePartInfo = spareParts.FirstOrDefault();

            if (sparePartInfo is not null)
            {
                return new SparePart()
                {
                    Id = sparePartInfo.Id.ToString(),
                    Name = sparePartInfo.Name,
                    Price = sparePartInfo.Price,
                    Model = _models.GetModelById(sparePartInfo.ModelId),
                    Count = sparePartInfo.Count,
                    Number = sparePartInfo.Number
                };
            }

            return null;
        }

        public async Task<SparePart> GetSparePartByNumberAsync(string number)
        {
            var sparePart = await _spareParts
                .FindAsync<SparePartDb>(x => x.Number == number);

            var sparePartInfo = sparePart.FirstOrDefault();

            if (sparePartInfo is not null)
            {
                return new SparePart()
                {
                    Id = sparePartInfo.Id.ToString(),
                    Name = sparePartInfo.Name,
                    Price = sparePartInfo.Price,
                    Count = sparePartInfo.Count,
                    Model = _models.GetModelById(sparePartInfo.ModelId),
                    Number = sparePartInfo.Number
                };
            }

            return null;
        }

        public async Task<List<SparePart>> GetSparePartsByModelAsync(Model model)
        {
            var spareParts = await _spareParts
                .FindAsync<SparePartDb>(x => x.ModelId == model.Id);

            return spareParts
                .ToEnumerable()
                .Select(x => new SparePart()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Count = x.Count,
                    Model = _models.GetModelById(x.ModelId),
                    Number = x.Number,
                }).ToList<SparePart>();
        }

        public async Task UpdateSparePartAsync(string id, SparePart sparePart)
        {
            var sparePartDb = new SparePartDb() 
            { 
                Id = ObjectId.Parse(sparePart.Id),
                Name = sparePart.Name,
                Price = sparePart.Price,
                Count = sparePart.Count,
                ModelId = sparePart.Model.Id,
                Number = sparePart.Number
            };

            var filter = Builders<SparePartDb>.Filter.Eq("_id", new ObjectId(id));

            await _spareParts.ReplaceOneAsync(filter, sparePartDb);
        }

        public async Task UpdateSparePartCountAsync(string id, int count)
        {
            var sparePart = GetSparePartById(id);
            sparePart.Count = count;

            await UpdateSparePartAsync(id, sparePart);
        }
    }
}
