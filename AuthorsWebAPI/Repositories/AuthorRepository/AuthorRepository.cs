using AuthorsWebAPI.Data;
using AuthorsWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthorsWebAPI.Repositories.AuthorRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AuthorApiContext _context;
        public AuthorRepository(AuthorApiContext context) => _context = context;

        public async Task<IEnumerable<Authors>> GetAllAsync() =>
            await _context.Authors.Include(a => a.Books).ToListAsync();
        public async Task<Authors> GetByIdAsync(int id) =>
            await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        public async Task<IEnumerable<Authors>> FindByNameAsync(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return await GetAllAsync();

            var normalized = name.Trim().ToLower();
            return await _context.Authors
                .Where(a => a.Name == normalized)
                .Include(a => a.Books)
                .ToListAsync();
        }
        public async Task<Authors> AddAsync(Authors author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();   
            return author;
        }
        public async Task<bool> UpdateAsync(Authors author)
        {
            var exists = await _context.Authors.AnyAsync(a => a.Id == author.Id);
            if (!exists)
            {
                return false;
            }
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id) {
            var author = await _context.Authors.FindAsync(id);
            if(author != null){return false;}

            _context.Authors.Remove(author);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
