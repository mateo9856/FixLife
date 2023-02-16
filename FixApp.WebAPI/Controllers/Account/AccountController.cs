using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiQueries.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Account
{

    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IClientIdentityService _clientIdentityService;

        public AccountController(IClientIdentityService clientIdentityService) {
            _clientIdentityService= clientIdentityService;
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

        
        [HttpGet("Account/Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
