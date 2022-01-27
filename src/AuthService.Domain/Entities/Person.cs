using AuthService.Domain.Validations.Persons;

namespace AuthService.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public Person(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            EntityValidation(this, new PersonValidation());
        }

        public Person Update(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            EntityValidation(this, new PersonValidation());

            return this;
        }
    }
}
