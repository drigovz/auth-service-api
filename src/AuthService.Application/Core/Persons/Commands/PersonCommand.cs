using MediatR;

namespace AuthService.Application.Core.Persons.Commands
{
    public class PersonCommand : IRequest<GenericResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
