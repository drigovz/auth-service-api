using MediatR;

namespace AuthService.Application.Core.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<GenericResponse>
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
