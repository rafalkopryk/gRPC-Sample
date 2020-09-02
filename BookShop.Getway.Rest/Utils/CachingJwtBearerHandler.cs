namespace BookShop.Getway.Rest.Utils
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    
    public class CachingJwtBearerHandler : JwtBearerHandler
    {
        private readonly IMemoryCache memoryCache;

        private readonly IConfiguration configuration;

        public CachingJwtBearerHandler(IConfiguration configuration, IMemoryCache memoryCache, IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.memoryCache = memoryCache;
            this.configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationToken = Request.Headers[HeaderNames.Authorization];
            if (!this.memoryCache.TryGetValue(authorizationToken, out var cachedAuthResult))
            {
                var cacheTime = configuration.GetValue<int>("Authentication:CacheTime");
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheTime));

                var authResult = await base.HandleAuthenticateAsync()
                    .ConfigureAwait(false);

                if (authResult.Succeeded)
                {
                    memoryCache.Set(authorizationToken, authResult, cacheEntryOptions);
                }

                return authResult;
            }

            return cachedAuthResult as AuthenticateResult;
        }
    }
}
