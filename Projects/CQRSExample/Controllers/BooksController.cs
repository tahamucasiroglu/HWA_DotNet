using CQRSExample.CQRS.Commands;
using CQRSExample.CQRS.Queries;
using CQRSExample.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            var books = await _mediator.Send(new GetBooksQuery());
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateBookCommand command)
        {
            var bookId = await _mediator.Send(command);
            return Ok(bookId);
        }
    }
}
