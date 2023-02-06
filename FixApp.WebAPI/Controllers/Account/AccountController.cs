using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Account
{
    public class AccountController : ControllerBase
    {

        public AccountController() {
        
        }
        
        public IActionResult Login()
        {
            return Ok();
        }

    }
}
