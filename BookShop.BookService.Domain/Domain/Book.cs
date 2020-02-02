using BookShop.BookService.Domain.ValueObjects;
using BookShop.Common.Utils;
using System;

namespace BookShop.BookService.Domain.Domain
{
    public class Book
    {
        public int BookId { get; protected set; }

        public string Title { get; protected set; }

        public BookStatus Status { get; protected set; }

        public DateTime ReleaseDate { get; set; }
        
        protected Book()
        {
        }

        public Book(string title, DateTime releaseDate)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Status = BookStatus.Available;
        }

        public Result CanChangeStatus(BookStatus status = null)
        {
            return Status == BookStatus.Archive && status != BookStatus.Archive
                ? Result.Failure("Book is Archive")
                : Result.Success();
        }

        public void ChangeStatus(BookStatus status)
        {
            var canChangeStatusOfBook = CanChangeStatus(status);
            if (canChangeStatusOfBook.IsFailure)
            {
                throw new Exception(canChangeStatusOfBook.Error.Description);
            }

            Status = status;
        }
    }
}
