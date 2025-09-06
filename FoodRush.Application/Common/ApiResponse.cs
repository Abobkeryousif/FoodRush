namespace FoodRush.Application.Common
{
    public class ApiResponse<T> where T : class
    {

        public ApiResponse(HttpStatusCode StatusCode, string message)
            {
                statusCode = (int)StatusCode;
                Message = message;
            }
        
        public ApiResponse(HttpStatusCode StatusCode, string message, T date)
        {
                statusCode = (int)StatusCode;
                Message = message;
                Data = date;
        }
        public int statusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
