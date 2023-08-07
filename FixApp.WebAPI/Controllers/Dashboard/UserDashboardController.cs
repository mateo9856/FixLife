using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserDashboardController(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("getdashboarddata")]
        public async Task<ActionResult> GetDashboardData()
        {
            var userId = User.Claims.FirstOrDefault(d => d.Type == "UserId")?.Value;
            var query = new GetDashboardDataQuery();
            query.UserId = userId;
            var response = await _mediator.Send(query);
            if (response.Item1 == 200)
                return Ok(_mapper.Map<GetDashboardQueryResponse>(response.Item2));
            return BadRequest();
        }

        [Authorize]
        [HttpGet("detectToNotification")]
        public async Task<ActionResult> DetectToPush()
        {

            return BadRequest();
        }

    }
}
