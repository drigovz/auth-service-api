using AuthService.Application.Core.Auth.Commands;
using AuthService.Application.Core.Users.Queries;
using AuthService.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Core.Auth.Handlers.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notification;

        public ForgotPasswordCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> sigInManager, IMediator mediator, NotificationContext notification)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _mediator = mediator;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery
            {
                Email = request.Email,
            });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Result);

            // send email service 

            return new GenericResponse 
            {
                Result = new
                {
                    Token = token,
                    Message = "Forgot password e-mail sended!",  
                },
            };
        }
    }
}
