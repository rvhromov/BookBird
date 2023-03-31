using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Authors.Commands;
using BookBird.Application.DTOs.Authors;
using BookBird.Application.Helpers;
using BookBird.Infrastructure.QueryHandlers.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator) => 
            _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IPaginatedList<AuthorDto>>> Get([FromQuery] GetAuthors request) => 
            Ok(await _mediator.Send(request));

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorWithBooksDto>> Get(Guid id) => 
            Ok(await _mediator.Send(new GetAuthor(id)));

        [HttpPost]
        public async Task<IActionResult> Post(CreateAuthor request)
        {
            var authorId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = authorId}, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateAuthor request)
        {
            var updateRequest = request with {Id = id};
            return Ok(await _mediator.Send(updateRequest));
        }
    }
}