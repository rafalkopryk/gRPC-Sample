namespace BookShop.BookService.Application.Extensions
{
    using System;

    using BookShop.BookService.Domain.Domain;
    using BookShop.Getway.Application.Handlers.Books;
    using Elasticsearch.Net;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nest;

    public static class BookServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElasticSearch(configuration);

            services.AddMediatR(typeof(AddBookHandler).Assembly);

            services.AddDbContext<UnitOfWork>(options => options.UseInMemoryDatabase("Books"));
        }

        private static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["Elasticsearch:Url"];
            var defaultIndex = "books";

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Book>(m => m
                    .IndexName(defaultIndex)
                    .IdProperty(p => p.BookId));

            var client = new ElasticClient(settings);
            var createIndexResponse = client.Indices.Create(
                    defaultIndex,
                    index => index.Map<Book>(device => device.AutoMap()));

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
