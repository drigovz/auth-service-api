using AuthService.Application.Core.Auth.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Auth.Validators
{
    public class ValidateTokenCommandValidator : AbstractValidator<ValidateTokenCommand>
    {
        public ValidateTokenCommandValidator()
        {
            RuleFor(x => x.Token).NotNull().NotEmpty();
        }
    }
}
