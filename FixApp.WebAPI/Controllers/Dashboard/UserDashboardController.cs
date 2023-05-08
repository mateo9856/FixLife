using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserDashboardController(IMediator mediator) {
            _mediator= mediator;
        }

        [Authorize]
        [HttpGet("getdashboarddata")]
        public async Task<ActionResult> GetDashboardData()
        {
            //TODO: Create CQRS
            return Ok();
        }

    }
}
