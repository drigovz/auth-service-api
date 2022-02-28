using AuthService.Application.Core.Auth.Queries;
using AuthService.Application.Core.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateCommand request)
            => Ok(await _mediator.Send(request));

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetUser([FromRoute] GetUserByIdQuery request)
            => Ok(await _mediator.Send(request));
    }
}
