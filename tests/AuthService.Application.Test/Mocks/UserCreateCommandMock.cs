using AuthService.Application.Core.Users.Commands;
using Bogus;

namespace AuthService.Application.Test.Mocks
{
    public abstract class UserCreateCommandMock
    {
        private static readonly Faker faker = new("en");
        private static string _password = faker.Lorem.Letter(10);
        private static string Email = faker.Internet.Email();
        private static string Password = _password;
        private static string ConfirmPassword = _password;

        public static UserCreateCommand UserCreateCommand()
        {
            return new UserCreateCommand
            {
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
            };
        }
    }
}
