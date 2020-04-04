using MediatR;

namespace ChatApi.Application.Messages.Queries
{
    public class GetAllMessagesQuery : IRequest<MessagesListViewModel>
    {
    }
}