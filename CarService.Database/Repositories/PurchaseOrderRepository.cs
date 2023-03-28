using CarService.Database.Models;
using CarService.Domain.Enums;
using CarService.Domain.Models;
using CarService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarService.Database.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private IMongoCollection<PurchaseOrderDb> _purchaseOrders;
        private IUserRepository _userRepository;
        private IClientRepository _clientRepository;
        private IDefectRepository _defectRepository;
        private IRepairRepository _repairRepository;
        private ISparePartRepository _sparePartRepository;
        private ICarRepository _carRepository;

        public PurchaseOrderRepository(MongoConnection connection, IUserRepository userRepository,
            IClientRepository clientRepository, IDefectRepository defectRepository, IRepairRepository repairRepository,
            ISparePartRepository sparePartRepository, ICarRepository carRepository)
        {
            _purchaseOrders = connection.Database.GetCollection<PurchaseOrderDb>("PurchaseOrders");
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _defectRepository = defectRepository;
            _repairRepository = repairRepository;
            _sparePartRepository = sparePartRepository;
            _carRepository = carRepository;
        }
        public async Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            var purchaseOrderdb = GetPurchaseOrderDb(purchaseOrder);

            await _purchaseOrders.InsertOneAsync(purchaseOrderdb);
        }

        public async Task<List<PurchaseOrderTableItem>> GetAllPurchaseOrdersAsync()
        {
            var purchaseOrders = await _purchaseOrders
                .FindAsync<PurchaseOrderDb>(x => true);

            return purchaseOrders
                .ToEnumerable()
                .Select(x => new PurchaseOrderTableItem()
                {
                    PurchaseOrderId = x.Id.ToString(),
                    Client = $"{_clientRepository.GetClientById(x.ClientId).Surname} " +
                    $"{_clientRepository.GetClientById(x.ClientId).Name}",
                    CreatedDate = x.CreatedDate,
                    Car = _carRepository.GetCarById(x.CarId).Model.Brand.Name + " "
                        + _carRepository.GetCarById(x.CarId).Model.Name + " "
                        + _carRepository.GetCarById(x.CarId).StateNumber,
                    TotalCost =  GetTotalCost(x.CompletedWorks, x.SpareParts),
                    Status = (OrderStatus) x.Status,
                }).ToList<PurchaseOrderTableItem>();
        }

        public PurchaseOrder GetPurchaseOrderById(string id)
        {
            var filter = Builders<PurchaseOrderDb>.Filter.Eq("_id", new ObjectId(id));

            var purchaseOrders = _purchaseOrders
                .Find<PurchaseOrderDb>(filter);

            var purchaseOrderInfo = purchaseOrders.FirstOrDefault();

            if (purchaseOrderInfo is not null)
            {
                return GetPurchaseOrder(purchaseOrderInfo);
            }

            return null;
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByIdAsync(string id)
        {
            var filter = Builders<PurchaseOrderDb>.Filter.Eq("_id", new ObjectId(id));

            var purchaseOrders = await _purchaseOrders
                .FindAsync<PurchaseOrderDb>(filter);

            var purchaseOrderInfo = purchaseOrders.FirstOrDefault();

            if (purchaseOrderInfo is not null)
            {
                var purchaseOrder = GetPurchaseOrder(purchaseOrderInfo);
                purchaseOrder.Id = purchaseOrderInfo.Id.ToString();
                return purchaseOrder;
            }

            return null;
        }

        public async Task UpdatePurchaseOrderAsync(string id, PurchaseOrder purchaseOrder)
        {
            var purchaseOrderDb = GetPurchaseOrderDb(purchaseOrder);
            purchaseOrderDb.Id = ObjectId.Parse(purchaseOrder.Id);

            var filter = Builders<PurchaseOrderDb>.Filter.Eq("_id", new ObjectId(id));

            await _purchaseOrders.ReplaceOneAsync(filter, purchaseOrderDb);
        }

        private PurchaseOrderDb GetPurchaseOrderDb(PurchaseOrder purchaseOrder)
        {
            return new PurchaseOrderDb()
            {
                CreatedDate = purchaseOrder.CreatedDate,
                Status = (int)purchaseOrder.Status,
                ClientId = purchaseOrder.Client.Id,
                CarId = purchaseOrder.Car.Id,
                ManagerId = purchaseOrder.Manager.Id,
                DiagnosticianId = purchaseOrder.Diagnostician.Id,
                MechanicId = purchaseOrder.Mechanic.Id,
                Defects = purchaseOrder.Defects?.Select(x => x.Id).ToList(),
                CompletedWorks = purchaseOrder
                                .CompletedWorks?
                                .Select(x => new RepairListItemDb()
                                {
                                    RepairId = x.Repair.Id,
                                    Count = x.Count,
                                    Price = x.Price
                                }).ToList(),
                SpareParts = purchaseOrder
                                .SpareParts?
                                .Select(x => new SparePartListItemDb()
                                {
                                    SparePartId = x.SparePart.Id,
                                    Count = x.Count,
                                    Price = x.Price
                                }).ToList(),
            };
        }

        private PurchaseOrder GetPurchaseOrder(PurchaseOrderDb purchaseOrderDb)
        {
            return new PurchaseOrder()
            {
                Id = purchaseOrderDb.Id.ToString(),
                CreatedDate = purchaseOrderDb.CreatedDate,
                Status = (OrderStatus) purchaseOrderDb.Status,
                Client = _clientRepository.GetClientById(purchaseOrderDb.ClientId),
                Car = _carRepository.GetCarById(purchaseOrderDb.CarId),
                Manager = _userRepository.GetUserById(purchaseOrderDb.ManagerId)!,
                Diagnostician = _userRepository.GetUserById(purchaseOrderDb.DiagnosticianId)!,
                Mechanic = _userRepository.GetUserById(purchaseOrderDb.MechanicId)!,
                Defects = purchaseOrderDb.Defects?.Select(x => _defectRepository.GetDefectById(x)).ToList(),
                CompletedWorks = purchaseOrderDb
                                .CompletedWorks?
                                .Select(x => new RepairListItem()
                                {
                                    Repair = _repairRepository.GetRepairById(x.RepairId),
                                    Count = x.Count,
                                    Price = x.Price
                                }).ToList(),
                SpareParts = purchaseOrderDb
                                .SpareParts?
                                .Select(x => new SparePartListItem()
                                {
                                    SparePart =_sparePartRepository.GetSparePartById(x.SparePartId),
                                    Count = x.Count,
                                    Price = x.Price
                                }).ToList(),
            };
        }

        private int GetTotalCost(List<RepairListItemDb>? repairs, List<SparePartListItemDb>? spareParts)
        {
            int repairRes = 0;
            int sparePartsRes = 0;

            if (repairs is not null)
                repairRes = repairs.Sum(x => x.Count * x.Price);
            
            if (spareParts is not null)
                sparePartsRes = repairs.Sum(x => x.Count * x.Price);

            return repairRes + sparePartsRes;
        }

        public bool IsPurchaseOrderExistByStatusAndDiagnostician(OrderStatus status, string diagnosticianId)
        {
            var purchaseOrders = _purchaseOrders
                .Find<PurchaseOrderDb>(x => x.Status == (int)status && x.DiagnosticianId == diagnosticianId);

            var purchaseOrderInfo = purchaseOrders.FirstOrDefault();

            if (purchaseOrderInfo is not null)
            {
                return true;
            }

            return false;
        }

        public bool IsPurchaseOrderExistByStatusAndMechanic(OrderStatus status, string mechanicId)
        {
            var purchaseOrders = _purchaseOrders
                .Find<PurchaseOrderDb>(x => x.Status == (int)status && x.MechanicId == mechanicId);

            var purchaseOrderInfo = purchaseOrders.FirstOrDefault();

            if (purchaseOrderInfo is not null)
            {
                return true;
            }

            return false;
        }
    }
}
