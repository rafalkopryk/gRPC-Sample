﻿using BookShop.Getway.Application.Abstractions;
using BookShop.Getway.Application.Messages.Commands;
using BookShop.Getway.Application.Models.Books;
using BookShop.Getway.Rest.Dtos;
using BookShop.Getway.Rest.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Getway.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IMediator _mediator;

        public BooksController(IBookService bookService, IMediator mediator)
        {
            _bookService = bookService;
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(Envelope<IList<Book>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope<IList<Book>>), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookService
                .GetBooks()
                .ConfigureAwait(false);
            
            return FromResult(result);
        }

        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto request)
        {
            var result = await _mediator
                .Send(new AddBook(request.Title, request.ReleaseDate))
                .ConfigureAwait(false);
            
            return FromResult(result);
        }

        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> ArchiveBook(int id)
        {
            var result = await _mediator
                .Send(new ArchiveBook(id))
                .ConfigureAwait(false);

            return FromResult(result);
        }

        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("{id}/blocking")]
        public async Task<IActionResult> LockBook(int id)
        {
            var result = await _mediator
                .Send(new LockBook(id))
                .ConfigureAwait(false);

            return FromResult(result);
        }
    }
}