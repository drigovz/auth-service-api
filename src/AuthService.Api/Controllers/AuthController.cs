using AuthService.Application.Core.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] AuthLoginCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] ValidateTokenCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
            => Ok(await _mediator.Send(request));
    }
}
