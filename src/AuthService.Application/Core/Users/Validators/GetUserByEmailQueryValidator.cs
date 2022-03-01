using AuthService.Application.Core.Users.Queries;
using FluentValidation;

namespace AuthService.Application.Core.Users.Validators
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().EmailAddress();
        }
    }
}
