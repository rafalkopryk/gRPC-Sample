namespace BookShop.Getway.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Common.Utils;
    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Models.Books;

    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Configuration;

    internal class BookProviderCacheDecorator : IBookProvider
    {
        private readonly IBookProvider bookProvider;

        private readonly IDistributedCache distributedCache;

        private readonly IConfiguration configuration;

        public BookProviderCacheDecorator(IBookProvider bookProvider, IDistributedCache distributedCache, IConfiguration configuration)
        {
            this.bookProvider = bookProvider;
            this.distributedCache = distributedCache;
            this.configuration = configuration;
        }

        public async Task<Result<IReadOnlyList<Book>>> GetBooks()
        {
            const string CacheKey = "BookService_GetBooks";
            var result = await this.distributedCache.GetAsync<Result<IReadOnlyList<Book>>>(CacheKey);
            if (result is null)
            {
                var cacheTimeInSeconds = this.configuration.GetValue<int>("ExternalService:BookService:CacheTimeInSeconds");
                var cacheEntryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTimeInSeconds),
                };

                result = await this.bookProvider.GetBooks();
                await this.distributedCache.SetAsync(CacheKey, result, cacheEntryOptions);

                return result;
            }

            return result;
        }
    }
}
