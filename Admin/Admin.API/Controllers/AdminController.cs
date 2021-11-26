﻿using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "Health")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<int> Health()
        {
            var response = "Admin V3 is live!! and healthy too!!";
            _logger.LogInformation(response);
            return Ok(response);
        }
        
        [HttpPost("search",Name ="Search")]
        public async Task<ActionResult<List<Profile>>> Search(SearchProfileQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
