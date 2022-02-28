using AuthService.Application.Core.Auth.Commands;
using AuthService.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthService.Application.Core.Auth.Handlers.Commands
{
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, GenericResponse>
    {
        private static IConfiguration _configuration;
        private readonly NotificationContext _notification;

        public ValidateTokenCommandHandler(IConfiguration configuration, NotificationContext notification)
        {
            _configuration = configuration;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            string mySecret = _configuration["Jwt:Key"],
                   myIssuer = _configuration["TokenConfiguration:Issuer"],
                   myAudience = _configuration["TokenConfiguration:Audience"];

            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var result = tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                if (result != null)
                {
                    return new GenericResponse
                    {
                        Result = new
                        {
                            Email = result.Identity?.Name,
                            Claims = result.Claims?.ToDictionary(x => x.Type, x => x.Value),
                        }
                    };
                }
                else
                {
                    _notification.AddNotification("Validation token Error", "Response is null");

                    return new GenericResponse
                    {
                        Notifications = _notification.Notifications,
                    };
                }
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Validation token Error", ex.Message);

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }
        }
    }
}
