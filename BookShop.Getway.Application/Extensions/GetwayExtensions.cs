namespace BookShop.Getway.Application.Extensions
{
    using System;
    using System.Net.Http;

    using BookShop.BookService.Rpc;
    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Handlers.Books;
    using BookShop.Getway.Application.Messages.Commands;
    using BookShop.Getway.Application.Services;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class GetwayExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBookService(configuration)
                .AddCommandHandlers();
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddBook).Assembly);

            services.Decorate<IRequestHandler<AddBook, Result>, RpcExceptionCommandHandlerDecorator<AddBook>>();
            services.Decorate<IRequestHandler<LockBook, Result>, RpcExceptionCommandHandlerDecorator<LockBook>>();
            services.Decorate<IRequestHandler<ArchiveBook, Result>, RpcExceptionCommandHandlerDecorator<ArchiveBook>>();

            return services;
        }

        private static IServiceCollection AddBookService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<BookRpc.BookRpcClient>(s =>
            {
                s.Address = new Uri(configuration.GetSection("ExternalService:BookService:Url").Value);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                };

                return httpHandler;
            });

            services.AddTransient<IBookProvider, BookProvider>();
            services.Decorate<IBookProvider, BookProviderExceptionDecorator>();
            services.Decorate<IBookProvider, BookProviderCacheDecorator>();

            return services;
        }
    }
}
