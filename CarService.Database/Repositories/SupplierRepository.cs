using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private IMongoCollection<SupplierDb> _suppliers;
        private ISparePartRepository _sparePartRepository;

        public SupplierRepository(MongoConnection connection, ISparePartRepository sparePartRepository)
        {
            _suppliers = connection.Database.GetCollection<SupplierDb>("Suppliers");
            _sparePartRepository = sparePartRepository;
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            var supplierDb = new SupplierDb()
            {
                Name = supplier.Name,
                Inn = supplier.Inn,
                PhoneNumber = supplier.PhoneNumber,
                SpareParts = supplier.SpareParts?
                    .Select(x => new SupplierSparePartItemDb()
                    {
                        SparePartId = x.SparePart.Id,
                        Cost = x.Cost,
                    }).ToList(),
            };

            await _suppliers.InsertOneAsync(supplierDb);
        }

        public async Task<List<SupplierTableItem>> GetAllSuppliersAsync()
        {
            var suppliers = await _suppliers
                .FindAsync<SupplierDb>(x => true);

            return suppliers
                .ToEnumerable()
                .Select(x => new SupplierTableItem()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Inn = x.Inn,
                    PhoneNumber = x.PhoneNumber
                    
                }).ToList<SupplierTableItem>();
        }

        public Supplier GetSupplierById(string id)
        {
            var filter = Builders<SupplierDb>.Filter.Eq("_id", new ObjectId(id));

            var supplier = _suppliers
                .Find<SupplierDb>(filter);

            var supplierInfo = supplier.FirstOrDefault();

            if (supplierInfo is not null)
            {
                return new Supplier()
                {
                    Id = supplierInfo.Id.ToString(),
                    Name = supplierInfo.Name,
                    Inn = supplierInfo.Inn,
                    PhoneNumber = supplierInfo.PhoneNumber,
                    SpareParts = supplierInfo.SpareParts?
                        .Select(z => new SupplierSparePartItem()
                        {
                            SparePart = _sparePartRepository.GetSparePartById(z.SparePartId),
                            Cost = z.Cost
                        }).ToList(),
                };
            }

            return null;
        }

        public async Task<Supplier> GetSupplierByIdAsync(string id)
        {
            var filter = Builders<SupplierDb>.Filter.Eq("_id", new ObjectId(id));

            var supplier = await _suppliers
                .FindAsync<SupplierDb>(filter);

            var supplierInfo = supplier.FirstOrDefault();

            if (supplierInfo is not null)
            {
                return new Supplier()
                {
                    Id = supplierInfo.Id.ToString(),
                    Name = supplierInfo.Name,
                    Inn = supplierInfo.Inn,
                    PhoneNumber = supplierInfo.PhoneNumber,
                    SpareParts = supplierInfo.SpareParts?
                        .Select(z => new SupplierSparePartItem()
                        {
                            SparePart = _sparePartRepository.GetSparePartById(z.SparePartId),
                            Cost = z.Cost
                        }).ToList(),
                };
            }

            return null;
        }

        public async Task<Supplier> GetSupplierByInnAsync(string inn)
        {
            var supplier = await _suppliers
                .FindAsync<SupplierDb>(x => x.Inn == inn);

            var supplierInfo = supplier.FirstOrDefault();

            if (supplierInfo is not null)
            {
                return new Supplier()
                {
                    Id = supplierInfo.Id.ToString(),
                    Name = supplierInfo.Name,
                    Inn = supplierInfo.Inn,
                    PhoneNumber = supplierInfo.PhoneNumber,
                    SpareParts = supplierInfo.SpareParts?
                        .Select(z => new SupplierSparePartItem()
                        {
                            SparePart = _sparePartRepository.GetSparePartById(z.SparePartId),
                            Cost = z.Cost
                        }).ToList(),
                };
            }

            return null;
        }

        public string GetSupplierNameByIdAsync(string id)
        {
            var filter = Builders<SupplierDb>.Filter.Eq("_id", new ObjectId(id));

            var supplier = _suppliers
                .Find<SupplierDb>(filter);

            var supplierInfo = supplier.FirstOrDefault();

            if (supplierInfo is not null)
            {
                return supplierInfo.Name;
            }

            return null;
        }

        public async Task<List<SupplierSparePartItem>?> GetSupplierSparePartsAsync(string supplierId)
        {
            var filter = Builders<SupplierDb>.Filter.Eq("_id", new ObjectId(supplierId));

            var supplier = await _suppliers
                .FindAsync<SupplierDb>(filter);

            return supplier.FirstOrDefault().SpareParts?
                .Select(x => new SupplierSparePartItem()
                {
                    SparePart = _sparePartRepository.GetSparePartById(x.SparePartId),
                    Cost = x.Cost

                }).ToList<SupplierSparePartItem>();
        }

        public async Task UpdateSupplierAsync(string id, Supplier supplier)
        {
            var supplierDb = new SupplierDb()
            {   Id = ObjectId.Parse(supplier.Id),
                Name = supplier.Name,
                Inn = supplier.Inn,
                PhoneNumber = supplier.PhoneNumber,
                SpareParts = supplier.SpareParts?
                    .Select(x => new SupplierSparePartItemDb()
                    {
                        SparePartId = x.SparePart.Id,
                        Cost = x.Cost,
                    }).ToList(),
            };

            var filter = Builders<SupplierDb>.Filter.Eq("_id", new ObjectId(id));

            await _suppliers.ReplaceOneAsync(filter, supplierDb);
        }
    }
}
