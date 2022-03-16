using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Notifications;
using AuthService.Application.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Core.Users.Handlers.Command
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly NotificationContext _notification;
        private readonly ILogger<UserCreateCommandHandler> _logger;

        public UserCreateCommandHandler(NotificationContext notification, SignInManager<IdentityUser> sigInManager, UserManager<IdentityUser> userManager, ILogger<UserCreateCommandHandler> logger)
        {
            _notification = notification;
            _sigInManager = sigInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, Cryptography.HashPassword(request.Password));
            if (!result.Succeeded)
            {
                foreach (var error in result?.Errors)
                {
                    _logger.LogError($"Identity Error - {error.Code}", error.Description);

                    _notification.AddNotification($"Identity Error - {error.Code}", error.Description);
                }

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            await _sigInManager.SignInAsync(user, false);

            _logger.LogInformation("User created Successfully", user);

            return new GenericResponse
            {
                Result = user,
            };
        }
    }
}
