using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Application.Messages.Dtos;
using ChatApi.Application.Users.Dtos;
using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using MediatR;

namespace ChatApi.Application.Messages.Queries
{
    /// <summary>
    /// Query handler for getting all messages
    /// </summary>
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, MessagesListViewModel>
    {
        private readonly IAsyncRepository<Message> _messageRepository;

        public GetAllMessagesQueryHandler(IAsyncRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessagesListViewModel> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetAsync();

            var messagesDtos = messages.Select(m =>
                new MessageDto
                {
                    Id = m.Id,
                    Text = m.Text,
                    Time = m.Time,
                    User = new UserChatDto
                    {
                        Id = m.User.Id,
                        UserName = m.User.UserName
                    }
                }).ToArray();

            return new MessagesListViewModel(messagesDtos);
        }
    }
}