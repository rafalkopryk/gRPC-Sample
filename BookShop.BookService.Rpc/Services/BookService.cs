namespace BookShop.BookService.Rpc.Services
{
    using System.Threading.Tasks;

    using BookShop.BookService.Domain.Messages.Commands;
    using BookShop.BookService.Domain.Messages.Queries;
    using BookShop.BookService.Rpc.Extensions;
    using BookShop.BookService.Rpc.Mappers.Books;
    using Grpc.Core;
    using MediatR;

    public class BookService : BookRpc.BookRpcBase
    {
        private readonly IMediator mediator;

        public BookService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async override Task<GetBooksReply> GetBooks(GetBooksRequest request, ServerCallContext context)
        {
            var getBooksResult = await this.mediator
                .Send(new GetBooks(), cancellationToken: context.CancellationToken);

            if (!getBooksResult.IsSuccess)
            {
                throw new RpcException(getBooksResult.ToStatus(), getBooksResult.Error?.Description);
            }

            var books = getBooksResult.Value?
                .ToRpcModel();

            return new GetBooksReply
            {
                Books = { books },
            };
        }

        public async override Task<AddBookReply> AddBook(AddBookRequest request, ServerCallContext context)
        {
            var addBookCommand = new AddBook(request.Title, request.ReleaseDate.ToDateTime());

            var addBookResult = await this.mediator
                .Send(addBookCommand, cancellationToken: context.CancellationToken);

            if (!addBookResult.IsSuccess)
            {
                throw new RpcException(addBookResult.ToStatus(), addBookResult.Error?.Description);
            }

            return new AddBookReply();
        }

        public async override Task<ArchiveBookReply> ArchiveBook(ArchiveBookRequest request, ServerCallContext context)
        {
            var archiveBookCommand = new ArchiveBook(request.Id);

            var archiveBookResult = await this.mediator
                .Send(archiveBookCommand, context.CancellationToken);

            if (!archiveBookResult.IsSuccess)
            {
                throw new RpcException(archiveBookResult.ToStatus(), archiveBookResult.Error?.Description);
            }

            return new ArchiveBookReply();
        }

        public async override Task<LockBookReply> LockBook(LockBookRequest request, ServerCallContext context)
        {
            var lockBookCommand = new LockBook(request.Id);

            var lockBookResult = await this.mediator
                .Send(lockBookCommand, context.CancellationToken);

            if (!lockBookResult.IsSuccess)
            {
                throw new RpcException(lockBookResult.ToStatus(), lockBookResult.Error?.Description);
            }

            return new LockBookReply();
        }
    }
}
