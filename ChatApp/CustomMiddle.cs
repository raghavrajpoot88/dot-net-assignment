using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ChatApp
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddle
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomMiddle(RequestDelegate next , ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger("CustomMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Custom Middleware Initiate");

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddleExtensions
    {
        public static IApplicationBuilder UseCustomMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddle>();
        }
    }
}
