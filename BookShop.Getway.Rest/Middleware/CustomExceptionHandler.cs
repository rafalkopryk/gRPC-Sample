﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BookShop.Getway.Rest.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookShop.Getway.Rest.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
            finally
            {
                _logger.LogInformation($"Request:{httpContext.Request}|Response:{httpContext.Response}");
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            var json = JsonSerializer.Serialize(Envelope.Error(e));
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsync(json);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
