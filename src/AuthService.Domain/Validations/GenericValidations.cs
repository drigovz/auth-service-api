using FluentValidation;

namespace AuthService.Domain.Validations
{
    public static class GenericValidations
    {
        public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.NotNull().NotEmpty().WithMessage("First Name is required!");

        public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.NotNull().NotEmpty().WithMessage("Last Name is required!");

        public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.NotNull().NotEmpty().WithMessage("E-mail is required!")
                          .EmailAddress().WithMessage("E-mail address not valid!");
    }
}
