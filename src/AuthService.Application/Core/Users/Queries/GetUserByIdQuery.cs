using MediatR;

namespace AuthService.Application.Core.Users.Queries
{
    public class GetUserByIdQuery : IRequest<GenericResponse>
    {
        public string Id { get; set; }
    }
}
