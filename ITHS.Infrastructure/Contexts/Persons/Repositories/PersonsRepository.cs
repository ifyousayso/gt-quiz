using ITHS.Domain.Entities;
using ITHS.Domain.Interfaces.Repositories;
using ITHS.Webapi.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ITHS.Infrastructure.Contexts.Persons.Repositories
{
    internal class PersonsRepository : IPersonsRepository
    {
        private readonly ITHSDatabaseContext _context;

        public PersonsRepository(ITHSDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Person> AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Person> GetAsync(Guid id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            var result = await _context.Persons.FindAsync(person.Id);
            if (result != null)
            {
                result.FirstName = person.FirstName;
                result.LastName = person.LastName;
                result.PhoneNumber = person.PhoneNumber;
                result.EmailAddress = person.EmailAddress;
                result.RoleId = person.RoleId;
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<Person>> FindPersonsByFirstName(string firstName)
        {
            var persons = await _context.Persons
                  .Where(person => person.FirstName.Contains(firstName))
                  .ToListAsync();

            return persons;
        }

    }
 
}
