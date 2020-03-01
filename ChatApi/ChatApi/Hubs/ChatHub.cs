using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IAsyncRepository<Message> _messageRepository;

        public ChatHub(IAsyncRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Send(Message message)
        {
            message = await _messageRepository.InsertAsync(message).ConfigureAwait(false);

            await Clients.All.SendAsync("received", message).ConfigureAwait(false);
        }
    }
}