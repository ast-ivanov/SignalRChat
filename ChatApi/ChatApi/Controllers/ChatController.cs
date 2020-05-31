using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApi.Application.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ChatApi.Application.Users.Dtos;
using System.Linq;
using System.Security.Claims;

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
        [Authorize]
        public ActionResult<UserChatDto> GetCurrentUser()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userName = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            return Ok(new UserChatDto
            {
                Id = userId,
                UserName = userName
            });
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<MessagesListViewModel>> GetMessages()
        {
            var messagesListViewModel = await _mediator.Send(new GetAllMessagesQuery()).ConfigureAwait(false);

            return Ok(messagesListViewModel);
        }
    }
}
