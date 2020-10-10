namespace BookShop.Getway.Rest.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
