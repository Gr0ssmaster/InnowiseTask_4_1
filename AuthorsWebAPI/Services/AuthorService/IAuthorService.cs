using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<IEnumerable<Authors>> GetAllAsync();
        Task<Authors?> GetByIdAsync(int id);
        Task<IEnumerable<Authors>> FindByNameAsync(string name);
        Task<(bool Success, string? Error, Authors? author)> AddAsync(Authors author);
        Task<bool> UpdateAsync(int id, Authors author);
        Task<bool> DeleteAsync(int id);
    }
}
