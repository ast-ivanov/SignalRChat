using System.Threading.Tasks;
using ChatApi.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return Ok(new {result.Token});
            }

            return BadRequest(new {result.ErrorMessage});
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegistrationCommand registrationCommand)
        {
            var result = await _mediator.Send(registrationCommand).ConfigureAwait(false);
            
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new {result.Errors});
        }
    }
}