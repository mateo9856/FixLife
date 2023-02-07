using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Account
{

    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public AccountController() {

        }

        [HttpPost("Account/Login")]
        public IActionResult Login()
        {
            return Ok();
        }

        
        [HttpGet("Account/Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
