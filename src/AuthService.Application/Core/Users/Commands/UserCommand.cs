using MediatR;

namespace AuthService.Application.Core.Users.Commands
{
    public class UserCommand : IRequest<GenericResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
