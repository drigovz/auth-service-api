using Bogus;
using Person = AuthService.Domain.Entities.Person;

namespace AuthService.Domain.Test.Builders
{
    public class PersonBuilder
    {
        private static readonly Faker faker = new("en");
        private string FirstName = faker.Name.FirstName();
        private string LastName = faker.Name.LastName();
        private string Email = faker.Internet.Email();

        public static PersonBuilder New()
            => new();

        public PersonBuilder PersonWithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        public PersonBuilder PersonWithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }

        public PersonBuilder PersonWithEmail(string email)
        {
            Email = email;
            return this;
        }

        public Person Build()
            => new(FirstName, LastName, Email);
    }
}
