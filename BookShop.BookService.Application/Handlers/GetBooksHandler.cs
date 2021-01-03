namespace BookShop.BookService.Application.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using BookShop.BookService.Domain.Domain;
    using BookShop.BookService.Domain.Messages.Queries;
    using BookShop.Common.Utils;
    using Microsoft.EntityFrameworkCore;
    using Nest;

    using Result = Common.Utils.Result;

    public class GetBooksHandler : IQueryHandler<GetBooks, IReadOnlyList<Book>>
    {
        private readonly UnitOfWork unitOfWork;

        private readonly IElasticClient elasticClient;

        public GetBooksHandler(UnitOfWork unitOfWork, IElasticClient elasticClient)
        {
            this.unitOfWork = unitOfWork;
            this.elasticClient = elasticClient;
        }

        public async Task<Result<IReadOnlyList<Book>>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            //var books = await this.unitOfWork
            //    .Books
            //    .Where(x => x.Status.Status != StatusBookEnum.Archive)
            //    .AsNoTracking()
            //    .ToListAsync() as IReadOnlyList<Book>;

            var books = (await this.elasticClient.SearchAsync<Book>(s => s
                .Query(q => q
                    .Bool(qs => qs
                        .MustNot(f => f.Term(t => t.Status.Status, StatusBookEnum.Archive))))))
                        .Documents;

            return Result.Success(books as IReadOnlyList<Book>);
        }
    }
}
