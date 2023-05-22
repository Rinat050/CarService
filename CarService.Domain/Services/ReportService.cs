using CarService.Domain.Models;
using CarService.Domain.Response;
using CarService.Domain.Services.Interfaces;

namespace CarService.Domain.Services
{
    public class ReportService : IReportService
    {
        private IPurchaseOrderService _purchaseOrderService;
        private ISupplierOrderService _supplierOrderService;

        public ReportService(IPurchaseOrderService purchaseOrderService, ISupplierOrderService supplierOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
            _supplierOrderService = supplierOrderService;
        }

        public async Task<BaseResponse<CashFlowReport>> GetCashFlowReportAsync(DateTime from, DateTime to)
        {
            try
            {
                var purchaseOrdersResult = await _purchaseOrderService.GetPurchaseOrdersByDateAsync(from, to);
                var supplierOrdersResult = await _supplierOrderService.GetSuppliersOrdersByDateAsync(from, to);

                if (!purchaseOrdersResult.Success || !supplierOrdersResult.Success)
                {
                    return new BaseResponse<CashFlowReport>()
                    {
                        Success = false,
                        Description = "Не удалось составить отчет!"
                    };
                }

                var report = new CashFlowReport()
                {
                    PurchaseOrdersInfo = purchaseOrdersResult.Data!.Select(x => new PurchaseOrderReportItem()
                    {
                        PurchaseOrderId = x.PurchaseOrderId,
                        CreatedDate = x.CreatedDate,
                        TotalCost = x.TotalCost,
                    }).ToList(),
                    SupplierOrdersInfo = supplierOrdersResult.Data!.Select(x => new SupplierOrderReportItem()
                    {
                        Id = x.Id,
                        TotalCost = x.TotalCost,
                        CreatedDate = x.CreatedDate
                    }).ToList()
                };

                return new BaseResponse<CashFlowReport>()
                {
                    Success = true,
                    Data = report
                };
            }
            catch
            {
                return new BaseResponse<CashFlowReport>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }

        public async Task<BaseResponse<RepairsReport>> GetRepairsReportAsync(DateTime from, DateTime to)
        {
            try
            {
                var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersInfoByDateAsync(from, to);

                if (!purchaseOrders.Success)
                {
                    return new BaseResponse<RepairsReport>()
                    {
                        Success = false,
                        Description = "Не удалось составить отчет!"
                    };
                }
                var report = new RepairsReport()
                {
                    ReportInfo = new()
                };

                foreach(var order in purchaseOrders.Data!)
                {
                    foreach (var work in order.CompletedWorks!)
                    {
                        var existRepair = report.ReportInfo
                                .FirstOrDefault(z => z.Repair.Id == work.Repair.Id);

                        if (existRepair != null)
                        {
                            existRepair.Count += work.Count;
                            existRepair.TotalCost += work.Count * work.Price;
                        }
                        else
                        {
                            report.ReportInfo.Add(new RepairsReportItem()
                            {
                                Repair = work.Repair,
                                Count = work.Count,
                                TotalCost = work.Count * work.Price
                            });
                        }
                    }
                }

                return new BaseResponse<RepairsReport>()
                {
                    Success = true,
                    Data = report
                };
            }
            catch
            {
                return new BaseResponse<RepairsReport>()
                {
                    Success = false,
                    Description = "Внутренняя ошибка!"
                };
            }
        }
    }
}
