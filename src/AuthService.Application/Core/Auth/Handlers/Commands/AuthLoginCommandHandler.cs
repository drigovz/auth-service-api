using AuthService.Application.Core.Auth.Commands;
using AuthService.Application.Core.Users.Queries;
using AuthService.Application.Notifications;
using AuthService.Application.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Core.Auth.Handlers.Commands
{
    public class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notification;

        public AuthLoginCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> sigInManager, NotificationContext notification, IMediator mediator)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _notification = notification;
            _mediator = mediator;
        }

        public async Task<GenericResponse> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery
            {
                Email = request.UserName,
            });

            string password = Cryptography.HashPassword(request.Password);
            if (await _userManager.CheckPasswordAsync(user.Result, password) == false)
            {
                _notification.AddNotification("Login Error", "Invalid credentials");

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            var result = await _sigInManager.PasswordSignInAsync(request.UserName, password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                _notification.AddNotification("Login error", "UserName or Password not valid!");

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            return new GenericResponse
            {
                Result = TokenConfig.GenerateToken(request.UserName),
            };
        }
    }
}
