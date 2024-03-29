﻿
namespace CarService.Domain.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string? Description { get; set; }
        public T? Data { get; set; }

        internal object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
