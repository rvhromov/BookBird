using System;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Feedbacks.Commands;
using BookBird.Application.DTOs.Feedbacks;
using BookBird.Infrastructure.QueryHandlers.Feedbacks.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBird.Api.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeedbacksController(IMediator mediator) => 
            _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDto>> Get(Guid id) =>
            Ok(await _mediator.Send(new GetFeedback(id)));

        [HttpPost]
        public async Task<IActionResult> Post(CreateFeedback request)
        {
            var feedbackId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = feedbackId}, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateFeedback request)
        {
            var updateRequest = request with {Id = id};
            return Ok(await _mediator.Send(updateRequest));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteFeedback(id));
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}