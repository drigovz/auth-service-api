using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Core.Users.Handlers.Command
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly NotificationContext _notification;

        public UserCreateCommandHandler(NotificationContext notification, UserManager<IdentityUser> userManager)
        {
            _notification = notification;
            _userManager = userManager;
        }

        public async Task<GenericResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            if (!(request.Password.Equals(request.ConfirmPassword)))
            {
                _notification.AddNotification("Password error", "Password dont match!");
                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            var result = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            });

            if (!result.Succeeded)
            {
                result.Errors.Select(error =>
                    _notification.AddNotification($"Identity Error - {error.Code}", error.Description)
                );

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            return new GenericResponse
            {
                Result = "User created successful!",
            };
        }
    }
}
