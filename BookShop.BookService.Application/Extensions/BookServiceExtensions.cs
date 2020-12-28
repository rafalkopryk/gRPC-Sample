namespace BookShop.BookService.Application.Extensions
{
    using BookShop.Getway.Application.Handlers.Books;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class BookServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddBookHandler).Assembly);

            services.AddDbContext<UnitOfWork>(options => options.UseInMemoryDatabase("Books"));
        }
    }
}
