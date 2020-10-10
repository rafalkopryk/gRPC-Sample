namespace BookShop.BookService.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;

    using CSharpFunctionalExtensions;

    public partial class BookStatus : ValueObject
    {
        protected BookStatus(StatusBookEnum status)
        {
            this.Status = status;
            this.StatusDate = DateTime.Now;
        }

        public static BookStatus Available => new (StatusBookEnum.Available);

        public static BookStatus Unavailable => new (StatusBookEnum.Unavailable);

        public static BookStatus Archive => new (StatusBookEnum.Archive);

        public StatusBookEnum Status { get; protected set; }

        public DateTime StatusDate { get; protected set; }

        public bool IsArchive => this.Status == StatusBookEnum.Archive;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Status;
        }
    }
}
