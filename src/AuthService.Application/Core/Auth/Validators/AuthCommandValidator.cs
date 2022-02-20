using AuthService.Application.Core.Auth.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Auth.Validators
{
    public class AuthCommandValidator : AbstractValidator<AuthCommand>
    {
        public AuthCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
