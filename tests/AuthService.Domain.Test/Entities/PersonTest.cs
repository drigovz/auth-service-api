using AuthService.Domain.Test.Builders;
using FluentAssertions;
using Xunit;

namespace AuthService.Domain.Test.Entities
{
    public class PersonTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Trait("Domain", "Person")]
        public void Should_Not_Create_Person_With_Invalid_FirstName(string invalidName)
        {
            var person = PersonBuilder.New().PersonWithFirstName(invalidName).Build();

            person.Valid.Should().BeFalse();
            person.ValidationResult?.Errors.Should().NotBeEmpty();
            person.ValidationResult?.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            person.ValidationResult?.Errors.Should().Contain(x => x.ErrorMessage.Contains("First Name"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [Trait("Domain", "Person")]
        public void Should_Not_Create_Person_With_Invalid_LastName(string invalidName)
        {
            var person = PersonBuilder.New().PersonWithLastName(invalidName).Build();

            person.Valid.Should().BeFalse();
            person.ValidationResult?.Errors.Should().NotBeEmpty();
            person.ValidationResult?.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            person.ValidationResult?.Errors.Should().Contain(x => x.ErrorMessage.Contains("Last Name"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("exemple")]
        [InlineData("exemple@")]
        [InlineData("@exemple")]
        [Trait("Domain", "Person")]
        public void Should_Not_Create_Person_With_Invalid_Email(string invalidEmail)
        {
            var person = PersonBuilder.New().PersonWithEmail(invalidEmail).Build();

            person.Valid.Should().BeFalse();
            person.ValidationResult?.Errors.Should().NotBeEmpty();
            person.ValidationResult?.Errors.Should().HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
            person.ValidationResult?.Errors.Should().Contain(x => x.ErrorMessage.Contains("E-mail"));
        }

        [Fact]
        public void Should_Create_Person()
        {
            var person = PersonBuilder.New().Build();

            person.Valid.Should().BeTrue();
            person.ValidationResult?.Errors.Should().BeEmpty();
        }
    }
}
