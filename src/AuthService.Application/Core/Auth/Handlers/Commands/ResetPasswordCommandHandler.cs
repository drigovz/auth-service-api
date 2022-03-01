using AuthService.Application.Core.Auth.Commands;
using AuthService.Application.Core.Users.Queries;
using AuthService.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Core.Auth.Handlers.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notification;

        public ResetPasswordCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> sigInManager, NotificationContext notification, IMediator mediator)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _notification = notification;
            _mediator = mediator;
        }

        public async Task<GenericResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery
            {
                Email = request.UserName,
            });

            var result = await _userManager.ResetPasswordAsync(user.Result, request.Token, request.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result?.Errors)
                    _notification.AddNotification($"Identity error {error.Code}", error.Description);

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            return new GenericResponse
            {
                Result = "Your password has been changed! Please login again!",
            };
        }
    }
}
