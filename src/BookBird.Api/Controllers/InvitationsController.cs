using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Invitations.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvitationsController(IMediator mediator) => 
            _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Post(CreateInvitation request)
        {
            var invitationId = await _mediator.Send(request);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> Post(AcceptInvitation request)
        {
            await _mediator.Send(request);
            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteInvitation(id));
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}