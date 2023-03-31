using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Genres.Commands;
using BookBird.Application.DTOs.Genres;
using BookBird.Application.Helpers;
using BookBird.Infrastructure.QueryHandlers.Genres.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IPaginatedList<GenreDto>>> Get([FromQuery] GetGenres request) =>
            Ok(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> Post(CreateGenre request)
        {
            var genreId = await _mediator.Send(request);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateGenre request)
        {
            var updateRequest = request with {Id = id};
            return Ok(await _mediator.Send(updateRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteGenre(id));
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}