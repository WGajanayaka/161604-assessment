using Mbrrace.Models;

namespace Mbrrace.DataAccess
{
    public interface IPersonRepository : IDisposable
    {
        public IQueryable<Person> GetPeople();
        Task<Person?> GetPersonByIDAsync(int id);
        Task InsertPersonAsync(Person person);
        void DeletePerson(int id);
        void UpdatePerson(Person person);
        Task SaveAsync();
    }
}
