using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.Application.Messages.Queries;
using MediatR;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<MessagesListViewModel>> GetMessages()
        {
            var messagesListViewModel = await _mediator.Send(new GetAllMessagesQuery()).ConfigureAwait(false);

            return Ok(messagesListViewModel);
        }
    }
}
