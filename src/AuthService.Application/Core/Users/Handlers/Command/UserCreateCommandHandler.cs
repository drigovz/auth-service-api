﻿using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Core.Users.Handlers.Command
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly NotificationContext _notification;

        public UserCreateCommandHandler(NotificationContext notification, SignInManager<IdentityUser> sigInManager, UserManager<IdentityUser> userManager)
        {
            _notification = notification;
            _sigInManager = sigInManager;
            _userManager = userManager;
        }

        public async Task<GenericResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                _notification.AddNotification("Password error", "Password dont match!");
                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                PasswordHash = request.Password,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result?.Errors)
                    _notification.AddNotification($"Identity Error - {error.Code}", error.Description);

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            await _sigInManager.SignInAsync(user, false);

            return new GenericResponse
            {
                Result = "User created successful!",
            };
        }
    }
}
