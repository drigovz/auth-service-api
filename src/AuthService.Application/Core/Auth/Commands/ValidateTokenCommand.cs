using MediatR;

namespace AuthService.Application.Core.Auth.Commands
{
    public class ValidateTokenCommand : IRequest<GenericResponse>
    {
        public string Token { get; set; }
    }
}
