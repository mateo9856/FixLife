using FixLife.WebApiInfra.Common.Constants;
using FixLife.WebApiQueries.AiClient.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.AiClient
{
    [Route("api/[controller]")]
    public class AiClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AiClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("freetimeRecommendations/{count}")]
        public async Task<IActionResult> GetFreeTimeRecommendations([FromRoute] int count)
        {
            var query = new GetFreeTimeRecomendationQuery
            {
                Count = count
            };
            var result = await _mediator.Send(query);

            return result.Item1 == HttpCodes.Ok ? Ok(result.Item2) : NotFound(result.Item2);
        }
    }
}
