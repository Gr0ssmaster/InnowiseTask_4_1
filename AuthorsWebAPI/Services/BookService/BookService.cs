using AuthorsWebAPI.Data;
using AuthorsWebAPI.Models;
using AuthorsWebAPI.Repositories.AuthorRepository;
using AuthorsWebAPI.Repositories.BookRepository;
using AuthorsWebAPI.Resources;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace AuthorsWebAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;


        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Books>> GetAllAsync() =>
            await _bookRepository.GetAllAsync();
        
        public async Task<Books?> GetByIdAsync(int id) =>
            await _bookRepository.GetByIdAsync(id);
        public async Task<IEnumerable<Books>> GetBooksAfterYearAsync(int year) =>
            await _bookRepository.GetBooksAfterYearAsync(year);
        public async Task<(bool Success, string? Error, Books? book)> AddAsync(Books book)
        {
            if (string.IsNullOrWhiteSpace(book.Title)) {
                return (false, ValidationMessages.BookTitleRequired, null);
            }
            var exists = await _bookRepository.GetByIdAsync(book.AuthorId);
            if(exists == null)
            {
                return (false, ValidationMessages.AuthorNotFound, null); 
            }

            var created = await _bookRepository.AddAsync(book);
            return (true, null, created);
        }

        public async Task<bool> UpdateAsync(int id, Books book)
        {
            var existing = await _bookRepository.GetByIdAsync(id);
            if (existing == null) {
                return false;
            }
            var authorExists = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (authorExists == null) return false;

            book.Id = id;
            return await _bookRepository.UpdateAsync(book);
        }
        public async Task<bool> DeleteAsync(int id) =>
            await _bookRepository.DeleteAsync(id);
    }
}
