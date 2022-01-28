using AuthService.Domain.Validations;
using AuthService.Application.Core.Persons.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Persons.Validators
{
    public class PersonRemoveCommandValidator : AbstractValidator<PersonRemoveCommand>
    {
        public PersonRemoveCommandValidator()
        {
            RuleFor(x => x.Id.ToString()).IsValidGuid();
        }
    }
}
