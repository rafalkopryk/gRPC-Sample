﻿using BookShop.BookService.Rpc;
using BookShop.Common.Utils;
using BookShop.Getway.Application.Abstractions;
using BookShop.Getway.Application.Handlers;
using BookShop.Getway.Application.Handlers.Books;
using BookShop.Getway.Application.Messages.Commands;
using BookShop.Getway.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookShop.Getway.Application.Extensions
{
    public static class BookServiceConfig
    {
        public static void AddMessages(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddBook).Assembly);
            
            services.Decorate<IRequestHandler<AddBook, Result>, RpcExceptionCommandHandlerDecorator<AddBook>>();
            services.Decorate<IRequestHandler<LockBook, Result>, RpcExceptionCommandHandlerDecorator<LockBook>>();
            services.Decorate<IRequestHandler<ArchiveBook, Result>, RpcExceptionCommandHandlerDecorator<ArchiveBook>>();
        }

        public static void AddBookService(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddGrpcClient<BookRpc.BookRpcClient>(s =>
            {
                s.Address = new Uri(configuration.GetSection("BookServiceUrl").Value);
            });

            services.AddTransient<IBookService, BookService>();
            services.Decorate<IBookService, BookServiceRpcExceptionDecorator>();
        }
    }
}
