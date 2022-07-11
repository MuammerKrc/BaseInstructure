using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{
    public class Response<T>
    {
        public bool IsSuccess { get; private set; }
        public T Model { get; private set; }
        public int StatusCode { get; private set; } = 200;
        public ErrorDto Error { get; private set; }

        public static Response<T> SuccessResponse(T model, int statusCode = 200)
        {
            return new Response<T>() { IsSuccess = true, Model = model, StatusCode = statusCode };
        }
        public static Response<T> SuccessResponse(int statusCode = 200)
        {
            return new Response<T>() { IsSuccess = true, StatusCode = statusCode, };
        }
        public static Response<T> ErrorResponse(ErrorDto errors, int statusCode = 500)
        {
            return new Response<T>() { Error = errors, StatusCode = statusCode, IsSuccess = false };
        }

        public static Response<T> ErrorResponse(string error, int statusCode = 500)
        {
            return new Response<T>() { Error = new ErrorDto(error: error), StatusCode = statusCode, IsSuccess = false };
        }
    }
}
