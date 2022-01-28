using AuthService.Application.Core.Persons.Commands;
using AuthService.Domain.Validations;
using FluentValidation;

namespace AuthService.Application.Core.Persons.Validators
{
    public class PersonUpdateCommandValidator : AbstractValidator<PersonUpdateCommand>
    {
        public PersonUpdateCommandValidator()
        {
            RuleFor(x => x.Id.ToString()).IsValidGuid();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.Email).Email();
        }
    }
}
