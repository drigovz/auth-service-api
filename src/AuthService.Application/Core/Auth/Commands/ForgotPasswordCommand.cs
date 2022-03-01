using MediatR;

namespace AuthService.Application.Core.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest<GenericResponse>
    {
        public string Email { get; set; }
    }
}
