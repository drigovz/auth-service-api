using AuthService.Application.Core.Users.Commands;
using AuthService.Application.Core.Users.Handlers.Command;
using AuthService.Application.Notifications;
using AuthService.Application.Test.Mocks;
using AuthService.Application.Utilities;
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

        [Fact]
        public void Should_Be_Possible_To_Create_New_User()
        {
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            _sigInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                             .ReturnsAsync(SignInResult.Success);

            _userCreateCommandHandler = _userCreateCommandHandlerMock.Object;

            var result = _userCreateCommandHandler.Handle(_userCreateCommand, new CancellationToken()).Result;

            string? id = result.Result?.Id;
            string? email = result.Result?.Email;
            string? userName = result.Result?.UserName;
            bool emailConfirmed = result.Result?.EmailConfirmed;

            result.Result?.Equals(true);
            email?.Should().NotBeNull();
            userName?.Should().NotBeNull();
            emailConfirmed.Should().BeTrue();
            id?.Should().NotBeNull();
            result.Notifications?.Should().BeEmpty();
            result.Notifications?.Should().HaveCount(c => c == 0);
        }
    }
}
