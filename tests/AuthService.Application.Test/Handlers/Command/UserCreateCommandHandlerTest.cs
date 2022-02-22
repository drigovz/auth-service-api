using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Core.Users.Handlers.Command;
using AuthService.Application.Notifications;
using AuthService.Application.Test.Mocks;
using AuthService.Application.Utilities;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Bogus;

namespace AuthService.Application.Test.Handlers.Command
{
    public class UserCreateCommandHandlerTest
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<SignInManager<IdentityUser>> _sigInManagerMock;
        private readonly Mock<UserCreateCommandHandler> _userCreateCommandHandlerMock;
        private UserCreateCommandHandler _userCreateCommandHandler;
        private readonly Mock<NotificationContext> _notification;
        private UserCreateCommand _userCreateCommand;

        public UserCreateCommandHandlerTest()
        {
            var _userManagerMock = A.Fake<UserManager<IdentityUser>>();
            var _sigInManagerMock = A.Fake<SignInManager<IdentityUser>>();
            _notification = new Mock<NotificationContext>();
            _userCreateCommand = UserCreateCommandMock.UserCreateCommand();

            _userCreateCommandHandlerMock = new Mock<UserCreateCommandHandler>(
                _notification.Object,
                _sigInManagerMock,
                _userManagerMock
            );
        }

        [Fact]
        public void Should_Be_Possible_To_Create_New_User()
        {
            var faker = new Faker("en");
            var myConfiguration = new Dictionary<string, string>
            {
                {"Cryptography:Key", faker.Lorem.Letter(32)},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _userCreateCommandHandler = _userCreateCommandHandlerMock.Object;
            Cryptography.SetConfig(configuration);
            var result = _userCreateCommandHandler.Handle(_userCreateCommand, new CancellationToken());

            result.Result.Equals(true);
            //result.Errors?.Should().NotBeEmpty();
            //result.Errors?.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            //result.Errors?.Should().Contain(x => x.ErrorMessage.Contains("Password"));
        }
    }
}
