using AuthService.Application.Core.Users.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Users.Validators
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                    .NotNull()
                    .NotEmpty()
                    .Equal(x => x.Password)
                    .WithMessage("Password don't match!");
        }
    }
}
