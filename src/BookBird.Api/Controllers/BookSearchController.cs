using System.Threading.Tasks;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Infrastructure.QueryHandlers.BookSearch.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/books/search")]
    public class BookSearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookSearchController(IMediator mediator) => 
            _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<BookIndexDto>> Get([FromQuery] SearchBook request) => 
            Ok(await _mediator.Send(request));
    }
}