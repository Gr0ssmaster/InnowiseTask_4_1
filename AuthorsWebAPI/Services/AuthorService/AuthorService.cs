using AuthorsWebAPI.Data;
using AuthorsWebAPI.Models;
using AuthorsWebAPI.Resources;

namespace AuthorsWebAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContainer _store;
        private int _nextId;

        public AuthorService(DataContainer store)
        {
            _store = store;
            _nextId = _store.authors.Any() ? _store.authors.Max(b => b.Id) + 1 : 1;
        }

        public IEnumerable<Authors> GetAll()
        {
            return _store.authors;
        }

        public Authors GetById(int id)
        {
            return _store.authors.FirstOrDefault(b => b.Id == id);
        }

        public (bool Success, string? Error, Authors? author) Add(Authors author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
            {
                return (false, ValidationMessages.AuthorNameRequired, null);
            }

            if (author.DateOfBirth == default)
            {
                return (false, ValidationMessages.AuthorBirthRequired, null);
            }


            author.Id = _nextId++;
            _store.authors.Add(author);
            return (true,null, author);
        }

        public bool Update(int id, Authors author)
        {
            var existing = GetById(id);
            if (existing == null) { return false; }

            existing.Name = author.Name;
            existing.DateOfBirth = author.DateOfBirth;
            return true;
        }

        public bool Delete(int id) {
            var existing = GetById(id);
            if(existing == null) { return false; }

           _store.books.RemoveAll(b => b.AuthorId == id);
           _store.authors.Remove(existing);
           return true;
        
        }
    }
}
