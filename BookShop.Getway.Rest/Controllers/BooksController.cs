namespace BookShop.Getway.Rest.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Getway.Application.Abstractions;
    using BookShop.Getway.Application.Messages.Commands;
    using BookShop.Getway.Application.Models.Books;
    using BookShop.Getway.Rest.Dtos;
    using BookShop.Getway.Rest.Utils;

    using MediatR;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class BooksController : BaseController
    {
        private readonly IBookProvider bookService;
        private readonly IMediator mediator;

        public BooksController(IBookProvider bookService, IMediator mediator)
        {
            this.bookService = bookService;
            this.mediator = mediator;
        }

        [ProducesResponseType(typeof(Envelope<IReadOnlyList<Book>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope<IReadOnlyList<Book>>), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = await this.bookService
                .GetBooks();

            return this.FromResult(result);
        }

        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookInput request)
        {
            var result = await this.mediator
                .Send(new AddBook(request?.Title, request.ReleaseDate));

            return this.FromResult(result);
        }

        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> ArchiveBook(int id)
        {
            var result = await this.mediator
                .Send(new ArchiveBook(id));

            return this.FromResult(result);
        }

        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("{id}/blocking")]
        public async Task<IActionResult> LockBook(int id)
        {
            var result = await this.mediator
                .Send(new LockBook(id));

            return this.FromResult(result);
        }
    }
}
