namespace BookShop.BookService.Domain.Domain
{
    using System;

    public record BookStatus
    {
        protected BookStatus(StatusBookEnum status)
        {
            this.Status = status;
            this.StatusDate = DateTime.Now;
        }

        public static BookStatus Available => new (StatusBookEnum.Available);

        public static BookStatus Unavailable => new (StatusBookEnum.Unavailable);

        public static BookStatus Archive => new (StatusBookEnum.Archive);

        public StatusBookEnum Status { get; init; }

        public DateTime StatusDate { get; init; }

        public bool IsArchive => this.Status == StatusBookEnum.Archive;
    }
}
