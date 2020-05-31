using System;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Application.Messages.Dtos;
using ChatApi.Application.Users.Dtos;
using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using MediatR;

namespace ChatApi.Application.Messages.Commands
{
    /// <summary> Create message command </summary>
    public class CreateMessageCommand : IRequest<MessageDto>
    {
        /// <summary> Message text </summary>
        public string Text { get; set; }

        /// <summary> Send time </summary>
        public DateTime Time { get; set; }

        /// <summary> User </summary>
        public UserChatDto User { get; set; }

        /// <summary> Create message command handler </summary>
        public class Handler : IRequestHandler<CreateMessageCommand, MessageDto>
        {
            private readonly IAsyncRepository<Message> _messageRepository;

            public Handler(IAsyncRepository<Message> messageRepository)
            {
                _messageRepository = messageRepository;
            }

            public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var message = new Message
                {
                    Text = request.Text,
                    Time = request.Time,
                    User = new User
                    {
                        Id = request.User.Id,
                        UserName = request.User.UserName
                    }
                };

                var newMessage = await _messageRepository.InsertAsync(message);

                var userDto = new UserChatDto
                {
                    Id = newMessage.User.Id,
                    UserName = newMessage.User.UserName,
                };

                var messageDto = new MessageDto
                {
                    Id = newMessage.Id,
                    Text = newMessage.Text,
                    Time = newMessage.Time,
                    User = userDto
                };

                return messageDto;
            }
        }
    }
}