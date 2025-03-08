using Azure.Core;
using FixLife.WebApiQueries.Account;
using FixLife.WebApiQueries.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator) {
            _mediator= mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]ClientIdentityRequest request)
        {
            var tryLogin = await _mediator.Send(new LoginUserCommand(request));

            if(tryLogin.Status == 200 || tryLogin.Status == 404)
            {
                return Ok(tryLogin);
            }

            return BadRequest(tryLogin);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]ClientIdentityRegisterRequest request)
        {
            var tryRequest = await _mediator.Send(new AddClientUserCommand(request));

            if(tryRequest.Status == 200)
            {
                return Ok(tryRequest);
            }

            return BadRequest(tryRequest);

        }

        [Authorize]
        [HttpPost("LoginByOAuth")]
        public async Task<IActionResult> LoginByOAuth([FromBody] AddOAuthTokenCommand request)
        {
            var tryRequest = await _mediator.Send(request);

            if (tryRequest.Status == 200)
            {
                return Ok(tryRequest);
            }

            return BadRequest(tryRequest);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
