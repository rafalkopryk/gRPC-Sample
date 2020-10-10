namespace BookShop.Getway.Rest.Middleware
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    using BookShop.Getway.Rest.Utils;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class CustomExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext).ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                await HandleExceptionAsync(httpContext, e).ConfigureAwait(false);
            }
            finally
            {
                this.logger.LogInformation($"Request:{httpContext.Request}|Response:{httpContext.Response}");
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            var json = JsonSerializer.Serialize(Envelope.Error(e));
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsync(json);
        }
    }
}
