using System;
namespace Trivagogoro_Backend.Models.DTO
{
    public class ResponseData<T> where T : class
    {
        public int Code { get; set; } = 200;
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Ok!";
        public T? Data { get; set; }

        public ResponseData(int code, bool isSuccess, string message, T? data)
        {
            this.Code = code;
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
        }
    }
}

