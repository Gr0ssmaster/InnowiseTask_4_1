using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Services.BookService
{
    public interface IBookService
    {
        Task <IEnumerable<Books>> GetAllAsync();
        Task<Books?> GetByIdAsync(int id);
        Task<IEnumerable<Books>> GetBooksAfterYearAsync(int year);
        Task<(bool Success, string? Error, Books? book)> AddAsync(Books book);
        Task<bool> UpdateAsync(int id, Books book);
        Task<bool> DeleteAsync(int id);
    }
}
