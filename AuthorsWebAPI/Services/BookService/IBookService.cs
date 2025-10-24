using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Services.BookService
{
    public interface IBookService
    {
        IEnumerable<Books> GetAll();
        Books? GetById(int id);
        (bool Success, string? Error, Books? book) Add(Books book);
        bool Update(int id, Books book);
        bool Delete(int id);
    }
}
