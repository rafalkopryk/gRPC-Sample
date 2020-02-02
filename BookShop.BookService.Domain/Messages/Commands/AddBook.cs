﻿using System;

namespace BookShop.BookService.Domain.Messages.Commands
{
    public class AddBook: ICommand
    {
        public AddBook(string title, DateTime releaseDate)
            => (Title, ReleaseDate) = (title, releaseDate);

        public string Title { get; }

        public DateTime ReleaseDate { get; }
    }
}
