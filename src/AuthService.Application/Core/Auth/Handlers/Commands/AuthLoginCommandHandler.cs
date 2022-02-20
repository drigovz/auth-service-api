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
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user != null && !user.EmailConfirmed)
            {
                _notification.AddNotification("Login Error", "Email not confirmed yet");

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            string password = Cryptography.HashPassword(request.Password);
            if (await _userManager.CheckPasswordAsync(user, password) == false)
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
