using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infra.Data.Context;

namespace AuthService.Infra.Data.Repository.Persons
{
    public class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        { }
    }
}
