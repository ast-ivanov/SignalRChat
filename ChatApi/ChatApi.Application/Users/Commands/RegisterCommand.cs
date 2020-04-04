using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Application.Users.Dtos;
using ChatApi.Domain.Entities;
using ChatApi.Domain.Services;
using MediatR;

namespace ChatApi.Application.Users.Commands
{
    public class RegisterCommand : IRequest<RegisterCommandResult>
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public class Handler : IRequestHandler<RegisterCommand, RegisterCommandResult>
        {
            private readonly IAsyncRepository<User> _userRepository;

            public Handler(IAsyncRepository<User> userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var entity = new User
                             {
                                 Name = request.Name,
                                 Password = request.Password
                             };

                try
                {
                    var newEntity = await _userRepository.InsertAsync(entity);

                    return new RegisterCommandResult
                           {
                               Success = true,
                               CreatedId = newEntity.Id
                           };
                }
                catch (DuplicateNameException)
                {
                    return new RegisterCommandResult
                           {
                               Success = false,
                               ErrorMessage = "User with the same name already exists"
                           };
                }
                catch (Exception e)
                {
                    return new RegisterCommandResult
                           {
                               Success = false,
                               ErrorMessage = e.Message
                           };
                }
            }
        }
    }
}