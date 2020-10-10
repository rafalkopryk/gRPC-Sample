namespace BookShop.Getway.Rest.Utils
{
    using System;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;

    public class CachingJwtBearerHandler : JwtBearerHandler
    {
        private readonly IMemoryCache memoryCache;

        private readonly IConfiguration configuration;

        public CachingJwtBearerHandler(IConfiguration configuration, IMemoryCache memoryCache, IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            this.memoryCache = memoryCache;
            this.configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationToken = this.Request.Headers[HeaderNames.Authorization].FirstOrDefault();
            if (!this.memoryCache.TryGetValue(authorizationToken, out AuthenticateResult authenticateResult))
            {
                var cacheTimeInMinutes = this.configuration.GetValue<int>("Authentication:CacheTimeInMinutes");
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTimeInMinutes),
                };

                authenticateResult = await base.HandleAuthenticateAsync()
                    .ConfigureAwait(false);

                if (authenticateResult.Succeeded)
                {
                    this.memoryCache.Set(authorizationToken, authenticateResult, cacheEntryOptions);
                }

                return authenticateResult;
            }

            return authenticateResult;
        }
    }
}
