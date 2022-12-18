using ITHS.Domain.Entities;

namespace ITHS.Domain.Interfaces.Repositories
{
    public interface IPersonsRepository
    {
        Task<List<Person>> FindPersonsByFirstName(string firstName);

        Task<Person> AddAsync(Person person);
    }
}
