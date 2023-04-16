using FixLife.WebApiQueries.FirstPlan;
using FixLife.WebApiQueries.FirstPlan.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.FirstPlan
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstPlanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FirstPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createFirstPlan")]
        public async Task<ActionResult> CreateFirstPlan([FromBody]CreatePlanRequest request)
        {
            var response = await _mediator.Send(new AddPlanCommand(request));
            if(response != null)
            {
                return StatusCode(response.Status, response);
            }
            return BadRequest();
        }
    }
}
