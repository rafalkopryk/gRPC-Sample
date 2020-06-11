namespace BookShop.BookService.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using CSharpFunctionalExtensions;

    public class BookStatus : ValueObject
    {
        public StatusBookEnum Status { get; protected set; }

        public DateTime StatusDate { get; protected set; }

        protected BookStatus(StatusBookEnum status)
        {
            Status = status;
            StatusDate = DateTime.Now;
        }

        public static BookStatus Available => new (StatusBookEnum.Available);
        public static BookStatus Unavailable => new (StatusBookEnum.Unavailable);
        public static BookStatus Archive => new (StatusBookEnum.Archive);

        public bool IsArchive => Status == StatusBookEnum.Archive;

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
