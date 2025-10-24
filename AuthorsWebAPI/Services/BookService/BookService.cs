using AuthorsWebAPI.Data;
using AuthorsWebAPI.Models;
using AuthorsWebAPI.Resources;
using System.Data.SqlTypes;

namespace AuthorsWebAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContainer _store;
        private int _nextId;

        public BookService(DataContainer store)
        {
            _store = store;
            _nextId = _store.books.Any() ? _store.books.Max(b => b.Id) + 1 : 1;
        }

        public IEnumerable<Books> GetAll()
        {
            return _store.books;
        }
        public Books? GetById(int id)
        {
            return _store.books.FirstOrDefault(b => b.Id == id);
        }
        public (bool Success, string? Error, Books? book) Add(Books book)
        {
            if (string.IsNullOrWhiteSpace(book.Title)) {
                return (false, ValidationMessages.BookTitleRequired, null);
            }
            if(!_store.authors.Any(a => a.Id == book.AuthorId))
            {
                return (false, ValidationMessages.AuthorNotFound, null); 
            }

            book.Id = _nextId++;
            _store.books.Add(book);
            return (true, null, book);
        }

        public bool Update(int id, Books book)
        {
            var existing = GetById(id);
            if (existing == null) {
                return false;
            }

            if (!_store.authors.Any(a => a.Id == book.AuthorId))
            {
                return false;
            }

            existing.Title = book.Title;
            existing.PublishedYear = book.PublishedYear;
            existing.AuthorId = book.AuthorId;
            return true;
        }
        public bool Delete(int id) {
            var existing = GetById(id);
            if (existing == null) {
                return false;
            }

            _store.books.Remove(existing);
            return true;
        }
    }
}
