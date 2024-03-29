﻿using CarService.Domain.Enums;
using CarService.Domain.Models;

namespace CarService.Domain.Repositories
{
    public interface IPurchaseOrderRepository
    {
        public Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder);

        public Task<List<PurchaseOrderTableItem>> GetAllPurchaseOrdersAsync();

        public Task<List<PurchaseOrderTableItem>> GetPurchaseOrdersByDateAsync(DateTime from, DateTime to);
        public Task<List<PurchaseOrder>> GetPurchaseOrdersInfoByDateAsync(DateTime from, DateTime to);

        public Task<List<PurchaseOrderTableItem>> GetPurchaseOrdersByDiagnosticianIdAsync(string diagnosticianId);

        public Task<List<PurchaseOrderTableItem>> GetPurchaseOrdersByMechanicIdAsync(string mechanicId);

        public Task<PurchaseOrder> GetPurchaseOrderByIdAsync(string id);

        public PurchaseOrder GetPurchaseOrderById(string id);

        public bool IsPurchaseOrderExistByStatusAndDiagnostician(OrderStatus status, string diagnosticianId);

        public bool IsPurchaseOrderExistByStatusAndMechanic(OrderStatus status, string mechanicId);

        public Task UpdatePurchaseOrderAsync(string id, PurchaseOrder purchaseOrder);
    }
}
