using System;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using MediatR;

namespace ChatApi.Application.Messages.Commands
{
    /// <summary>
    /// Create message command
    /// </summary>
    public class CreateMessageCommand : IRequest<Guid>
    {
        /// <summary>
        /// Message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Send time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Create message command handler
        /// </summary>
        public class Handler : IRequestHandler<CreateMessageCommand, Guid>
        {
            private readonly IAsyncRepository<Message> _messageRepository;

            public Handler(IAsyncRepository<Message> messageRepository)
            {
                _messageRepository = messageRepository;
            }

            public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var message = new Message
                {
                    Text = request.Text,
                    Time = request.Time,
                    User = new User {Id = request.UserId}
                };

                var newMessage = await _messageRepository.InsertAsync(message);

                return newMessage.Id;
            }
        }
    }
}