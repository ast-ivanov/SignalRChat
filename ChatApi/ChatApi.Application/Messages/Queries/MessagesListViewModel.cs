using ChatApi.Application.Messages.Dtos;

namespace ChatApi.Application.Messages.Queries
{
    /// <summary>
    /// View model of messages list
    /// </summary>
    public class MessagesListViewModel
    {
        public MessagesListViewModel(MessageDto[] messages)
        {
            Messages = messages;
        }

        /// <summary>
        /// Messages
        /// </summary>
        public MessageDto[] Messages { get; }
    }
}