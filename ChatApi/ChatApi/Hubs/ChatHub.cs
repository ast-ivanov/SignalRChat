using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ChatApi.Application.Messages.Commands;
using ChatApi.Application.Messages.Dtos;
using MediatR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Send(CreateMessageCommand createMessageCommand)
        {
            var message = await _mediator.Send(createMessageCommand).ConfigureAwait(false);

            await Clients.All.SendAsync("received", message).ConfigureAwait(false);
        }
    }
}