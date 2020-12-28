namespace BookShop.BookService.Domain.Domain
{
    using System;

    using BookShop.Common.Utils;

    public class Book
    {
        public Book(string title, DateTime releaseDate)
        {
            this.Title = title;
            this.ReleaseDate = releaseDate;
            this.Status = BookStatus.Available;
        }

        protected Book()
        {
        }

        public int BookId { get; protected set; }

        public string Title { get; protected set; }

        public BookStatus Status { get; protected set; }

        public DateTime ReleaseDate { get; protected set; }

        public Result CanChangeStatus(BookStatus status)
        {
            return this.Status.IsArchive is true && status?.IsArchive is not true
                ? Result.Failure("Book is Archive")
                : Result.Success();
        }

        public void ChangeStatus(BookStatus status)
        {
            var canChangeStatusOfBook = this.CanChangeStatus(status);
            if (!canChangeStatusOfBook.IsSuccess)
            {
                throw new Exception(canChangeStatusOfBook.Error.Description);
            }

            this.Status = status;
        }
    }
}
