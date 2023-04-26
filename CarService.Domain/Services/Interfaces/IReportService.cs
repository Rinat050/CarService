using CarService.Domain.Models;
using CarService.Domain.Response;

namespace CarService.Domain.Services.Interfaces
{
    public interface IReportService
    {
        public Task<BaseResponse<CashFlowReport>> GetCashFlowReportAsync(DateTime from, DateTime to);
    }
}