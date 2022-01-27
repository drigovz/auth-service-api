using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person, Guid>
    { }
}
