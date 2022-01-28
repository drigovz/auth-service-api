using MediatR;

namespace AuthService.Application.Core.Persons.Commands
{
    public class PersonRemoveCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
    }
}
