using CarService.Domain.Models;
using CarService.Domain.Response;
using System;
namespace CarService.Domain.Services.Interfaces
{
    public interface IModelService
    {
        public Task<BaseResponse<Model>> CreateModelAsync(Model model);

        public Task<BaseResponse<Model>> UpdateModelAsync(Model model);

        public Task<BaseResponse<List<Model>>> GetAllModelsAsync();
    }
}
