using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Features.Commands.AddProfile;
using Profile.Application.Features.Commands.UpdateProfile;
using System.Net;
using System.Threading.Tasks;

namespace Profile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
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

        [HttpPost(Name = "AddProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddProfile([FromBody] AddProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateProfile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
