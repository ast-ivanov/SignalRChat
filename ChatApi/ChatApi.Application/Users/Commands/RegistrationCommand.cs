using ChatApi.Application.Users.Dtos;
using ChatApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApi.Application.Users.Commands
{
    public class RegistrationCommand : IRequest<RegistrationCommandResult>
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public class Handler : IRequestHandler<RegistrationCommand, RegistrationCommandResult>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<RegistrationCommandResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    UserName = request.Name
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                return new RegistrationCommandResult
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors?.Select(er => er.Description).ToArray()
                };
            }
        }
    }
}
