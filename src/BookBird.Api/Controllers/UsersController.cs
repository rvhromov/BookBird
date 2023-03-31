using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Users.Commands;
using BookBird.Application.DTOs.Meetings;
using BookBird.Application.DTOs.Users;
using BookBird.Application.Helpers;
using BookBird.Infrastructure.QueryHandlers.Meetings.Queries;
using BookBird.Infrastructure.QueryHandlers.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => 
            _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IPaginatedList<UserDto>>> Get([FromQuery] GetUsers request) =>
            Ok(await _mediator.Send(request));

        [HttpGet("{id}/meetings")]
        public async Task<ActionResult<IPaginatedList<MeetingDto>>> Get(
            [FromRoute] Guid id, [FromQuery] GetOwnMeetings request)
        {
            var getRequest = request with {UserId = id};
            return Ok(await _mediator.Send(getRequest));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUser request)
        {
            var userId = await _mediator.Send(request);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateUser request)
        {
            var updateRequest = request with {Id = id};
            return Ok(await _mediator.Send(updateRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUser(id));
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}