using Admin.Application.Features.Queries.SearchQuery;
using Admin.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "Health")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> Health()
        {
            var response = "I am Good!!";
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
