using AuthorsWebAPI.Data;
using AuthorsWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthorsWebAPI.Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly AuthorApiContext _context;
        public BookRepository(AuthorApiContext context) =>
            _context = context;
        public async Task<IEnumerable<Books>> GetAllAsync() =>
            await _context.Books.ToListAsync();
        public async Task<Books> GetByIdAsync(int id) =>
            await _context.Books.FirstOrDefaultAsync(a => a.Id == id);
        public async Task<IEnumerable<Books>> GetBooksAfterYearAsync(int year)=>
            await _context.Books.Where(a => a.PublishedYear > year).ToListAsync();
        
        public async Task<Books> AddAsync(Books book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<bool> UpdateAsync(Books book) {
            var exists = await _context.Books.AnyAsync(b => b.Id == book.Id);
            if (!exists) {
                return false; }

            _context.Books.Update(book);
            _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id) {
            var book = await _context.Books.AnyAsync(b => b.Id == id);
            if (book == null)
            {
                return false;
            }
            
            _context.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
