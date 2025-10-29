using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Repositories.BookRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Books>> GetAllAsync();
        Task<Books?> GetByIdAsync(int id);
        Task<IEnumerable<Books>> GetBooksAfterYearAsync(int year);
        Task<Books> AddAsync(Books book);
        Task<bool> UpdateAsync(Books book);
        Task<bool> DeleteAsync(int id);

    }
}
