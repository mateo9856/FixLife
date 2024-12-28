using FixLife.WebApiQueries.FirstPlan;
using FixLife.WebApiQueries.FirstPlan.Commands;
using FixLife.WebApiQueries.FirstPlan.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.Plan
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("createFirstPlan")]
        public async Task<ActionResult> CreateFirstPlan([FromBody]CreatePlanRequest request)
        {
            var userId = User.Claims.FirstOrDefault(d => d.Type == "UserId")?.Value;

            var response = await _mediator.Send(new AddPlanCommand(request, userId));
            if (response != null)
            {
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPut("EditPlan")]
        public async Task<ActionResult> EditPlan([FromBody]EditPlanRequest request)
        {
            var userId = User.Claims.FirstOrDefault(d => d.Type == "UserId")?.Value;
            var response = await _mediator.Send(new EditPlanCommand(request, userId));
            if (response != null)
            {
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("UserPlanId")]
        public async Task<ActionResult> GetUserPlanId()
        {
            var userId = User.Claims.FirstOrDefault(d => d.Type == "UserId")?.Value;
            var query = new GetUserPlanIdQuery
            {
                UserId = userId
            };
            var response = await _mediator.Send(query);

            return StatusCode(response.Item1, response.Item2);
        }
    }
}
