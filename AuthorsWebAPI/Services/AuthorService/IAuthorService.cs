using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Services.AuthorService
{
    public interface IAuthorService
    {
        IEnumerable<Authors> GetAll();
        Authors? GetById(int id);
        (bool Success, string? Error, Authors? author) Add(Authors author);
        bool Update(int id, Authors author);
        bool Delete(int id);
    }
}
