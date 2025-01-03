﻿using AutoMapper;
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
        public UserDashboardController(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
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
                return StatusCode(response.Item1, response.Item2);
            return BadRequest();
        }

        [Authorize]
        [HttpGet("detectToNotification")]
        public async Task<ActionResult> DetectToPush()
        {
            var userId = User.Claims.FirstOrDefault(d => d.Type == "UserId")?.Value;
            var query = new GetDetectionPushQuery();
            query.UserId = userId;
            var response = await _mediator.Send(query);
            if (response.Status == 200)
                return Ok(response);
            return BadRequest();
        }

    }
}
