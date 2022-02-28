using MediatR;

namespace AuthService.Application.Core.Auth.Queries
{
    public class GetUserByIdQuery : IRequest<GenericResponse>
    {
        public string Id { get; set; }
    }
}
