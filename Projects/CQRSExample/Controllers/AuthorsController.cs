using CQRSExample.CQRS.Commands;
using CQRSExample.CQRS.Queries;
using CQRSExample.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> Get()
        {
            return Ok(await _mediator.Send(new GetAuthorsQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateAuthorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
