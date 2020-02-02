using BookShop.BookService.Domain.Messages.Commands;
using BookShop.BookService.Domain.Messages.Queries;
using BookShop.BookService.Rpc.Mappers.Books;
using BookShop.BookService.Rpc.Utils;
using Grpc.Core;
using MediatR;
using System.Threading.Tasks;

namespace BookShop.BookService.Rpc.Services
{
    public class BookService : BookRpc.BookRpcBase
    {
        private readonly IMediator _mediator;

        public BookService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async override Task<GetBooksReply> GetBooks(GetBooksRequest request, ServerCallContext context)
        {
            var getBooksResult = await _mediator
                .Send(new GetBooks(), context.CancellationToken)
                .ConfigureAwait(false);

            if (getBooksResult.IsFailure)
                throw new RpcException(getBooksResult.ToStatus(), getBooksResult.Error?.Description);

            var books = getBooksResult.Value?
                .ToRpcModel();

            return new GetBooksReply
            {
                Books = { books }
            };
        }

        public async override Task<AddBookReply> AddBook(AddBookRequest request, ServerCallContext context)
        {
            var addBookCommand = new AddBook(request.Title, request.ReleaseDate.ToDateTime());
            
            var addBookResult = await _mediator
                .Send(addBookCommand, context.CancellationToken)
                .ConfigureAwait(false);

            if (addBookResult.IsFailure)
                throw new RpcException(addBookResult.ToStatus(), addBookResult.Error?.Description);

            return new AddBookReply();
        }

        public async override Task<ArchiveBookReply> ArchiveBook(ArchiveBookRequest request, ServerCallContext context)
        {
            var archiveBookCommand = new ArchiveBook(request.Id);

            var archiveBookResult = await _mediator
                .Send(archiveBookCommand, context.CancellationToken)
                .ConfigureAwait(false);

            if (archiveBookResult.IsFailure)
                throw new RpcException(archiveBookResult.ToStatus(), archiveBookResult.Error?.Description);

            return new ArchiveBookReply();
        }

        public async override Task<LockBookReply> LockBook(LockBookRequest request, ServerCallContext context)
        {
            var lockBookCommand = new LockBook(request.Id);

            var lockBookResult = await _mediator
                .Send(lockBookCommand, context.CancellationToken)
                .ConfigureAwait(false);

            if (lockBookResult.IsFailure)
                throw new RpcException(lockBookResult.ToStatus(), lockBookResult.Error?.Description);

            return new LockBookReply();
        }
    }
}
