using MediatR;

namespace AuthService.Application.Core.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<GenericResponse>
    {
        public string Email { get; set; }
    }
}
