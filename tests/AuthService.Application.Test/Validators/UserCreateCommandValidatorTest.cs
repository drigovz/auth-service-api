using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Core.Users.Validators;
using Bogus;
using FluentAssertions;
using Xunit;

namespace AuthService.Application.Test.Validators
{
    public class UserCreateCommandValidatorTest
    {
        private static readonly Faker faker = new("en");
        private static string _password = faker.Lorem.Letter(10);
        private string Email = faker.Internet.Email();
        private string Password = _password;
        private string ConfirmPassword = _password;

        private UserCreateCommandValidator _validator;
        private UserCreateCommand _userCreateCommand;

        public UserCreateCommandValidatorTest()
        {
            _validator = new UserCreateCommandValidator();
            _userCreateCommand = new UserCreateCommand
            {
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
            };
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("exemple")]
        [InlineData("exemple@")]
        [InlineData("@exemple")]
        [Trait("Application", "Identity User")]
        public void Should_Not_Create_Identity_User_With_Invalid_UserName(string invalidEmail)
        {
            _userCreateCommand.Email = invalidEmail;

            var result = _validator.ValidateAsync(_userCreateCommand).Result;

            result.IsValid.Should().BeFalse();
            result.Errors?.Should().NotBeEmpty();
            result.Errors?.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            result.Errors?.Should().Contain(x => x.ErrorMessage.Contains("Email"));
        }

        [Fact]
        [Trait("Application", "Identity User")]
        public void Should_Not_Create_Identity_User_With_Invalid_Password()
        {
            _userCreateCommand.Password = string.Empty;

            var result = _validator.ValidateAsync(_userCreateCommand).Result;

            result.IsValid.Should().BeFalse();
            result.Errors?.Should().NotBeEmpty();
            result.Errors?.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            result.Errors?.Should().Contain(x => x.ErrorMessage.Contains("Password"));
        }

        [Fact]
        [Trait("Application", "Identity User")]
        public void Should_Not_Create_Identity_User_With_Diferent_Passwords()
        {
            _userCreateCommand.ConfirmPassword = faker.Lorem.Letter(2);

            var result = _validator.ValidateAsync(_userCreateCommand).Result;

            result.IsValid.Should().BeFalse();
            result.Errors?.Should().NotBeEmpty();
            result.Errors?.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            result.Errors?.Should().Contain(x => x.ErrorMessage.Contains("don't match"));
        }
    }
}
