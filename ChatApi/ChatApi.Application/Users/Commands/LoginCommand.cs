using System.Threading;
using System.Threading.Tasks;
using ChatApi.Application.Users.Dtos;
using ChatApi.Domain.Services;
using MediatR;

namespace ChatApi.Application.Users.Commands
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginCommand, LoginCommandResult>
        {
            private readonly IUserManager _userManager;

            public Handler(IUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var (userId, message) = await _userManager.LoginAsync(request.Name, request.Password);

                return new LoginCommandResult
                       {
                           Success = userId != null,
                           ErrorMessage = message,
                           User = userId != null
                                      ? new UserChatDto
                                        {
                                            Id = userId.Value,
                                            Name = request.Name
                                        }
                                      : null
                       };
            }
        }
    }
}