namespace Akshada.API.models
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public APIResponse(int statusCode, T? data = default, string? message = null)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }
    }
}
