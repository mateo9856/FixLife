using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiQueries.Account;
using FixLife.WebApiQueries.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Account
{

    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IClientIdentityService _clientIdentityService;
        private readonly IMediator _mediator;

        public AccountController(IClientIdentityService clientIdentityService, IMediator mediator) {
            _clientIdentityService= clientIdentityService;
            _mediator= mediator;
        }

        [HttpPost("Account/Login")]
        public async Task<IActionResult> Login([FromBody]ClientIdentityRequest request)
        {
            var tryLogin = await _clientIdentityService.LoginAsync(request);

            if(tryLogin.Status == 200)
            {
                return Ok(tryLogin);
            }

            return BadRequest(tryLogin);
        }

        [HttpPost("Account/Register")]
        public async Task<IActionResult> Register([FromBody]ClientIdentityRegisterRequest request)
        {
            var tryRequest = await _mediator.Send(new AddClientUserCommand(request));

            if(tryRequest.Status == 200)
            {
                return Ok(tryRequest);
            }

            return BadRequest(tryRequest);

        }
        
        [HttpGet("Account/Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
