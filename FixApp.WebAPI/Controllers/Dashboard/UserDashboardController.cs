using FixLife.WebApiQueries.Dashboard.Queries;
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
            var userId = User.Claims.FirstOrDefault(d => d.Type == "UserId")?.Value;
            var query = new GetDashboardDataQuery();
            query.UserId = userId;
            var response = await _mediator.Send(query);
            //TODO: Returned plan without works, try use include() or another realtionship option
            return Ok(response);
        }

    }
}
