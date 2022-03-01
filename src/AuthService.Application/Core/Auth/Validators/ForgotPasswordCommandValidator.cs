using AuthService.Application.Core.Auth.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Auth.Validators
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
