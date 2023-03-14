using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private IMongoCollection<BrandDb> _brands;

        public BrandRepository(MongoConnection connection)
        {
            _brands = connection.Database.GetCollection<BrandDb>("Brands");
        }

        public async Task AddBrandAsync(Brand brand)
        {
            var brandDb = new BrandDb()
            {
                Name = brand.Name
            };

            await _brands.InsertOneAsync(brandDb);
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            var brands = await _brands
                .FindAsync<BrandDb>(x => true);

            return brands
                .ToEnumerable()
                .Select(x => new Brand()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name
                }).ToList<Brand>();
        }

        public Brand GetBrandById(string id)
        {
            var filter = Builders<BrandDb>.Filter.Eq("_id", new ObjectId(id));

            var brands = _brands
                .Find<BrandDb>(filter);

            var brandInfo = brands.FirstOrDefault();

            if (brandInfo is not null)
            {
                return new Brand()
                {
                    Id = brandInfo.Id.ToString(),
                    Name = brandInfo.Name,
                };
            }

            return null;
        }

        public async Task<Brand> GetBrandByIdAsync(string id)
        {
            var filter = Builders<BrandDb>.Filter.Eq("_id", new ObjectId(id));

            var brands = await _brands
                .FindAsync<BrandDb>(filter);

            var brandInfo = brands.FirstOrDefault();

            if (brandInfo is not null)
            {
                return new Brand()
                {
                    Id = brandInfo.Id.ToString(),
                    Name = brandInfo.Name,
                };
            }

            return null;
        }

        public async Task<Brand> GetBrandByNameAsync(string name)
        {
            var brand = await _brands
                .FindAsync<BrandDb>(x => x.Name == name);

            var brandInfo = brand.FirstOrDefault();

            if (brandInfo is not null)
            {
                return new Brand()
                {
                    Id = brandInfo.Id.ToString(),
                    Name = brandInfo.Name,
                };
            }

            return null;
        }

        public async Task UpdateBrandAsync(string id, Brand brand)
        {
            var brandDb = new BrandDb() { Id = ObjectId.Parse(brand.Id), Name = brand.Name };

            var filter = Builders<BrandDb>.Filter.Eq("_id", new ObjectId(id));

            await _brands.ReplaceOneAsync(filter, brandDb);
        }
    }
}
