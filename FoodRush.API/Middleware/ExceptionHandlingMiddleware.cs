

namespace FoodRush.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "Unhandled exception occurred while processing request { Path}", context.Request.Path);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(HttpStatusCode.InternalServerError,"An unexpected Error Occurred",ex.Message);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);

            }
        }


    }
}
