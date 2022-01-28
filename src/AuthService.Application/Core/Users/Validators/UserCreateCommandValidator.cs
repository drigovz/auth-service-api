using AuthService.Application.Core.Users.Commands;
using FluentValidation;

namespace AuthService.Application.Core.Users.Validators
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    { }
}
