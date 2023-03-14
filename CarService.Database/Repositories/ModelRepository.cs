using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private IMongoCollection<ModelDb> _models;
        private IBrandRepository _brands;

        public ModelRepository(MongoConnection connection, IBrandRepository brandRepository)
        {
            _models = connection.Database.GetCollection<ModelDb>("Models");
            _brands = brandRepository;
        }

        public async Task AddModelAsync(Model model)
        {
            var modelDb = new ModelDb()
            {
                Name = model.Name,
                BrandId = model.Brand.Id
            };

            await _models.InsertOneAsync(modelDb);
        }

        public async Task<List<Model>> GetAllModelsAsync()
        {
            var models = await _models
                .FindAsync<ModelDb>(x => true);

            return models
                .ToEnumerable()
                .Select(x => new Model()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Brand = _brands.GetBrandById(x.BrandId)
                }).ToList<Model>();
        }

        public async Task<Model> GetModelByIdAsync(string id)
        {
            var filter = Builders<ModelDb>.Filter.Eq("_id", new ObjectId(id));

            var models = await _models
                .FindAsync<ModelDb>(filter);

            var modelInfo = models.FirstOrDefault();

            if (modelInfo is not null)
            {
                return new Model()
                {
                    Id = modelInfo.Id.ToString(),
                    Name = modelInfo.Name,
                    Brand = await _brands.GetBrandByIdAsync(modelInfo.BrandId)
                };
            }

            return null;
        }

        public async Task<Model> GetModelByNameAsync(string name)
        {
            var model = await _models
                .FindAsync<ModelDb>(x => x.Name == name);

            var modelInfo = model.FirstOrDefault();

            if (modelInfo is not null)
            {
                return new Model()
                {
                    Id = modelInfo.Id.ToString(),
                    Name = modelInfo.Name,
                    Brand = await _brands.GetBrandByIdAsync(modelInfo.BrandId)
                };
            }

            return null;
        }

        public async Task UpdateModelAsync(string id, Model model)
        {
            var modelDb = new ModelDb() { Id = ObjectId.Parse(model.Id), Name = model.Name, BrandId = model.Brand.Id };

            var filter = Builders<ModelDb>.Filter.Eq("_id", new ObjectId(id));

            await _models.ReplaceOneAsync(filter, modelDb);
        }
    }
}
