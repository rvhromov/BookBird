using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Books.Commands;
using BookBird.Application.DTOs.Books;
using BookBird.Application.DTOs.Feedbacks;
using BookBird.Application.Helpers;
using BookBird.Infrastructure.QueryHandlers.Books.Queries;
using BookBird.Infrastructure.QueryHandlers.Feedbacks.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IPaginatedList<BookWithAuthorDto>>> Get([FromQuery] GetBooks request) =>
            Ok(await _mediator.Send(request));

        [HttpGet("{id}")]
        public async Task<ActionResult<BookExtendedDto>> Get(Guid id) =>
            Ok(await _mediator.Send(new GetBook(id)));

        [HttpGet("{id}/feedbacks")]
        public async Task<ActionResult<IPaginatedList<FeedbackDto>>> GetFeedbacks(
            [FromRoute] Guid id, [FromQuery] GetBookFeedbacks request)
        {
            var getRequest = request with {BookId = id};
            return Ok(await _mediator.Send(getRequest));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBook request)
        {
            var bookId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = bookId}, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, UpdateBook request)
        {
            var updateRequest = request with {Id = id};
            return Ok(await _mediator.Send(updateRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteBook(id));
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}