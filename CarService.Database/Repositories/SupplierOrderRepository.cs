using CarService.Database.Models;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class SupplierOrderRepository : ISupplierOrderRepository
    {
        private IMongoCollection<SupplierOrderDb> _orders;
        private ISparePartRepository _sparePartRepository;
        private ISupplierRepository _supplierRepository;

        public SupplierOrderRepository(MongoConnection connection, ISparePartRepository sparePartRepository,
                                        ISupplierRepository supplierRepository)
        {
            _orders = connection.Database.GetCollection<SupplierOrderDb>("SupplierOrders");
            _sparePartRepository = sparePartRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task AddSupplierOrderAsync(SupplierOrder supplierOrder)
        {
            var supplierOrderDb = new SupplierOrderDb()
            {
                SupplierId = supplierOrder.Supplier.Id,
                CreatedDate = supplierOrder.CreatedDate,
                SpareParts = supplierOrder.SpareParts
                    .Select(x => new SparePartListItemDb()
                    {
                        SparePartId = x.SparePart.Id,
                        Price = x.Price,
                        Count = x.Count,
                    }).ToList(),
            };

            await _orders.InsertOneAsync(supplierOrderDb);
        }

        public async Task<List<SupplierOrderTableItem>> GetAllSuppliersOrdersAsync()
        {
            var orders = await _orders
                .FindAsync<SupplierOrderDb>(x => true);

            return orders
                .ToEnumerable()
                .Select(x => new SupplierOrderTableItem()
                {
                    Id = x.Id.ToString(),
                    CreatedDate = x.CreatedDate,
                    Supplier = _supplierRepository.GetSupplierNameByIdAsync(x.SupplierId),
                    TotalCost = GetTotalCost(x.SpareParts),
                }).ToList<SupplierOrderTableItem>();
        }

        public async Task<SupplierOrder> GetSupplierOrderByIdAsync(string id)
        {
            var filter = Builders<SupplierOrderDb>.Filter.Eq("_id", new ObjectId(id));

            var order = await _orders
                .FindAsync<SupplierOrderDb>(filter);

            var orderInfo = order.FirstOrDefault();

            if (orderInfo is not null)
            {
                return new SupplierOrder()
                {
                    Id = orderInfo.Id.ToString(),
                    CreatedDate = orderInfo.CreatedDate,
                    Supplier = _supplierRepository.GetSupplierInfoById(orderInfo.SupplierId),
                    SpareParts = orderInfo.SpareParts
                        .Select(z => new SparePartListItem()
                        {
                            SparePart = _sparePartRepository.GetSparePartById(z.SparePartId),
                            Price = z.Price,
                            Count = z.Count
                        }).ToList(),
                };
            }

            return null;
        }

        private int GetTotalCost(List<SparePartListItemDb>? spareParts)
        {
            int sparePartsRes = 0;

            if (spareParts is not null)
                sparePartsRes = spareParts.Sum(x => x.Count * x.Price);

            return sparePartsRes;
        }
    }
}
