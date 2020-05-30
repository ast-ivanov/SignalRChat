using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Application.Models;
using ChatApi.Application.Users.Dtos;
using ChatApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatApi.Application.Users.Commands
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginCommand, LoginCommandResult>
        {
            private readonly UserManager<User> _userManager;
            private readonly ApplicationSettings _settings;

            public Handler(UserManager<User> userManager, IOptions<ApplicationSettings> options)
            {
                _userManager = userManager;
                _settings = options.Value;
            }

            public async Task<LoginCommandResult> Handle(LoginCommand loginCommand, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(loginCommand.Name);

                if (user == null || !await _userManager.CheckPasswordAsync(user, loginCommand.Password))
                {
                    return new LoginCommandResult
                    {
                        Succeeded = false,
                        ErrorMessage = "User name or password isn't correct"
                    };
                }
                var key = Encoding.UTF8.GetBytes(_settings.JWT_Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(_settings.TokenLifeSpanInDays),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(securityToken);

                return new LoginCommandResult
                {
                    Succeeded = true,
                    Token = token
                };
            }
        }
    }
}