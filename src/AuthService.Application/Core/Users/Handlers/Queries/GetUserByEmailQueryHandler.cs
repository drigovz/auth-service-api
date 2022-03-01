using AuthService.Application.Core.Users.Queries;
using AuthService.Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Core.Users.Handlers.Queries
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GenericResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly NotificationContext _notification;

        public GetUserByEmailQueryHandler(UserManager<IdentityUser> userManager, NotificationContext notification)
        {
            _userManager = userManager;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _notification.AddNotification("User not found", $"User with e-mail {request.Email} not found!");

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            return new GenericResponse
            {
                Result = user,
            };
        }
    }
}
