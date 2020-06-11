namespace BookShop.BookService.Application
{
    using BookShop.BookService.Domain.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.BookId);
            builder.OwnsOne(p => p.Status);
        }
    }
}