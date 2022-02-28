using AuthService.Application.Core.Auth.Queries;
using FluentValidation;

namespace AuthService.Application.Core.Auth.Validators
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
