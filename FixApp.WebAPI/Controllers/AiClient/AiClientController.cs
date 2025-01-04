using FixLife.WebApiInfra.Common.Constants;
using FixLife.WebApiQueries.AiClient.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixApp.WebAPI.Controllers.AiClient
{
    public class AiClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AiClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("weeklyWorkRecommendations")]
        public async Task<IActionResult> GetWeeklyWorkRecommendations()
        {
            var query = new GetWeeklyWorkRecomendationQuery();
            var result = await _mediator.Send(query);

            return result.Item1 == HttpCodes.Ok ? Ok(result.Item2) : NotFound(result.Item2);
        }
    }
}
