using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Meetings.Commands;
using BookBird.Application.DTOs.Invitations;
using BookBird.Application.DTOs.Meetings;
using BookBird.Application.DTOs.MeetingVisitors;
using BookBird.Application.Helpers;
using BookBird.Infrastructure.QueryHandlers.Invitations.Queries;
using BookBird.Infrastructure.QueryHandlers.Meetings.Queries;
using BookBird.Infrastructure.QueryHandlers.MeetingVisitors.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingsController(IMediator mediator) => 
            _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDto>> Get(Guid id) =>
            Ok(await _mediator.Send(new GetMeeting(id)));

        [HttpGet("{id}/invitations")]
        public async Task<ActionResult<IPaginatedList<InvitationDto>>> GetInvitations(
            [FromRoute] Guid id, [FromQuery] GetMeetingInvitations request)
        {
            var getRequest = request with {MeetingId = id};
            return Ok(await _mediator.Send(getRequest));
        }

        [HttpGet("{id}/visitors")]
        public async Task<ActionResult<IPaginatedList<MeetingVisitorDto>>> GetVisitors(
            [FromRoute] Guid id, [FromQuery] GetMeetingVisitors request)
        {
            var getRequest = request with {MeetingId = id};
            return Ok(await _mediator.Send(getRequest)); 
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMeeting request)
        {
            var meetingId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = meetingId}, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateMeeting request)
        {
            var updateRequest = request with {Id = id};
            return Ok(await _mediator.Send(updateRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteMeeting(id));
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}