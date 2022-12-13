using Mbrrace.Data;
using Mbrrace.Models;
using Microsoft.EntityFrameworkCore;

namespace Mbrrace.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MbrraceContext _context;

        public PersonRepository(MbrraceContext context)
        {
            _context = context;
        }
        public IQueryable<Person> GetPeople()
        {
            return _context.Person
                .Where(x => !x.IsDeleted);
        } 
        public async Task<Person?> GetPersonByIDAsync(int id)
        {
            return await _context.Person
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertPersonAsync(Person person)
        {
            person.CreatedDateTime = DateTime.Now;
            await _context.AddAsync(person);
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void UpdatePerson(Person person)
        {
            person.ModifiedDateTime = DateTime.Now;
            _context.Update(person);
        }

        public void DeletePerson(int id)
        {
             var person = _context.Person.Find(id);
            if (person != null)
            {
                person.DeletedDateTime = DateTime.Now;
                person.IsDeleted = true;
                _context.Person.Update(person);
               // _context.Remove(person);
            }
        }

        //Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
