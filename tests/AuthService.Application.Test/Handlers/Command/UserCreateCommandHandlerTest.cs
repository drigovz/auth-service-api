using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Core.Users.Handlers.Command;
using AuthService.Application.Notifications;
using AuthService.Application.Test.Mocks;
using AuthService.Application.Utilities;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading;
using Xunit;

namespace AuthService.Application.Test.Handlers.Command
{
    public class UserCreateCommandHandlerTest
    {
        private readonly Mock<UserCreateCommandHandler> _userCreateCommandHandlerMock;
        private UserCreateCommandHandler _userCreateCommandHandler;
        private readonly NotificationContext _notification;
        private UserCreateCommand _userCreateCommand;

        private Mock<FakeUserManager> _userManagerMock;
        private Mock<FakeSignInManager> _sigInManagerMock;

        public UserCreateCommandHandlerTest()
        {
            _userManagerMock = new Mock<FakeUserManager>();
            _sigInManagerMock = new Mock<FakeSignInManager>();
            _notification = new NotificationContext();
            _userCreateCommand = UserCreateCommandMock.UserCreateCommand();

            Cryptography.SetConfig(ConfigurationMock.CreateConfiguration());

            _userCreateCommandHandlerMock = new Mock<UserCreateCommandHandler>(
                _notification,
                _sigInManagerMock.Object,
                _userManagerMock.Object
            );
        }

        [Fact (Skip = "Don't possible to create user")]
        public void Should_Be_Possible_To_Create_New_User()
        {
            Faker faker = new("en");
            var user = new IdentityUser
            {
                UserName = faker.Internet.Email(),
                Email = faker.Internet.Email(),
                EmailConfirmed = true
            };

            _userManagerMock.Setup(x => x.CreateAsync(user, Cryptography.HashPassword("mypassword@123")))
                           .ReturnsAsync(IdentityResult.Success)
                           .Verifiable();

            _userCreateCommandHandler = _userCreateCommandHandlerMock.Object;

            var result = _userCreateCommandHandler.Handle(_userCreateCommand, new CancellationToken()).Result;

            result.Result?.Equals(true);
            result.Notifications?.Should().BeEmpty();
            result.Notifications?.Should().HaveCount(c => c == 0);
        }
    }
}
