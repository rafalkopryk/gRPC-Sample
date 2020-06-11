namespace BookShop.Getway.Application.Handlers.Books
{
    using System.Threading;
    using System.Threading.Tasks;
    using BookShop.BookService.Application;
    using BookShop.BookService.Application.Handlers;
    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.Common.Utils;

    public class AddBookHandler : ICommandHandler<AddBook>
    {
        private readonly UnitOfWork _unitOfWork;

        public AddBookHandler(UnitOfWork bookContext)
        {
            _unitOfWork = bookContext;
        }

        public async Task<Result> Handle(AddBook request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                return Result.Failure(new Error(ErrorCode.InvalidArgument, $"{nameof(request)} is null"));
            }

            var book = new Book(request.Title, request.ReleaseDate);

            _unitOfWork.Books.Add(book);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return Result.Success();
        }
    }
}
