namespace AuthService.Application.Core.Persons.Commands
{
    public class PersonUpdateCommand : PersonCommand
    {
        public int Id { get; set; }

        public PersonUpdateCommand(int id)
        {
            Id = id;
        }
    }
}
