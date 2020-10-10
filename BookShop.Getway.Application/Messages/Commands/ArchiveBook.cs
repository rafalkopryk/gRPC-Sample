namespace BookShop.Getway.Application.Messages.Commands
{
    public class ArchiveBook : ICommand
    {
        public ArchiveBook(int id)
            => this.Id = id;

        public int Id { get; }
    }
}
