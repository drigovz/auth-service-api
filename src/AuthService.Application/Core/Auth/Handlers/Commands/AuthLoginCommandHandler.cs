using AuthService.Application.Core.Auth.Commands;
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
        private readonly NotificationContext _notification;

        public AuthLoginCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> sigInManager, NotificationContext notification)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            var result = await _sigInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
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
