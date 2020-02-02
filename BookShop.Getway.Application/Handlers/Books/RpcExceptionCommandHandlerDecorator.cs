using BookShop.Common.Utils;
using BookShop.Getway.Application.Extensions;
using BookShop.Getway.Application.Messages;
using Grpc.Core;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Getway.Application.Handlers.Books
{
    public class RpcExceptionCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _commandHandler;

        public RpcExceptionCommandHandlerDecorator(ICommandHandler<T> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public async Task<Result> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                return await _commandHandler.Handle(request, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (RpcException exception)
            {
                var error = exception.Status.ToError();
                return Result.Failure(error);
            }
        }
    }
}
