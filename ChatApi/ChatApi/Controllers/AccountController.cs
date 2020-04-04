using System.Threading.Tasks;
using ChatApi.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand).ConfigureAwait(false);

            if (result.Success)
            {
                return Ok(new
                          {
                              result.Success,
                              result.User
                          });
            }

            return Ok(new
                      {
                          result.Success,
                          result.ErrorMessage
                      });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand).ConfigureAwait(false);

            if (result.Success)
            {
                return Ok(new
                          {
                              result.Success,
                              Id = result.CreatedId,
                              registerCommand?.Name
                          });
            }

            return Ok(new
                      {
                          result.Success,
                          result.ErrorMessage
                      });
        }
    }
}