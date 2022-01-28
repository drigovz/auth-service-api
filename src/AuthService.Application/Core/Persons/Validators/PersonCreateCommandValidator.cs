using AuthService.Domain.Validations;
using AuthService.Application.Core.Persons.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Persons.Validators
{
    public class PersonCreateCommandValidator : AbstractValidator<PersonCreateCommand>
    {
        public PersonCreateCommandValidator()
        {
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.Email).Email();
        }
    }
}
