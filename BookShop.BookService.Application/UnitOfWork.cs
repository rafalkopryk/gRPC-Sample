using BookShop.BookService.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BookShop.BookService.Application
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork([NotNullAttribute] DbContextOptions<UnitOfWork> options) : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
