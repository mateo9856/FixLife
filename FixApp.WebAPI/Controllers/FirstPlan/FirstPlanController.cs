using FixLife.WebApiQueries.FirstPlan;
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
            var response = _mediator.Send(request);
            return Created("", new {});
        }
    }
}
