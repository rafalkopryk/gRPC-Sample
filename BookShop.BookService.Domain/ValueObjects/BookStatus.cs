using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace BookShop.BookService.Domain.ValueObjects
{
    public class BookStatus : ValueObject
    {
        public StatusBookEnum Status { get; protected set; }

        public DateTime StatusDate { get; protected set; }

        protected BookStatus(StatusBookEnum status)
        {
            Status = status;
            StatusDate = DateTime.Now;
        }

        public static BookStatus Available => new BookStatus(StatusBookEnum.Available);
        public static BookStatus Unavailable => new BookStatus(StatusBookEnum.Unavailable);
        public static BookStatus Archive => new BookStatus(StatusBookEnum.Archive);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Status;
        }

        public enum StatusBookEnum
        {
            Available,
            Unavailable,
            Archive
        }
    }
}
