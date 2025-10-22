using System.Net;

namespace SmartBank.API.Middleware
{
    public class ExceptionsMiddleware :Exception
    {
        private readonly RequestDelegate _next;

        public ExceptionsMiddleware(RequestDelegate next)
        {
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
                await HandleExceptionAsync(context, ex);

            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Detail = exception.StackTrace
            };
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
