using BookShop.Getway.Application.Handlers.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.BookService.Application.Extensions
{
    public static class BookServiceConfig
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(AddBookHandler).Assembly);

            services.AddDbContext<UnitOfWork>(options => options.UseInMemoryDatabase("Books"));
        }
    }
}
