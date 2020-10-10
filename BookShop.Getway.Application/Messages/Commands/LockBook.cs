namespace BookShop.Getway.Application.Messages.Commands
{
    public class LockBook : ICommand
    {
        public LockBook(int id)
            => this.Id = id;

        public int Id { get; }
    }
}
