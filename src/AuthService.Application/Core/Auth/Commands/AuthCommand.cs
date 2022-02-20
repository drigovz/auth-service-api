using MediatR;

namespace AuthService.Application.Core.Auth.Commands
{
    public class AuthCommand : IRequest<GenericResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
