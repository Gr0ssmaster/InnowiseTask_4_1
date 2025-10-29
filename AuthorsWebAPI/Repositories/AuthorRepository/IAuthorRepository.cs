using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Repositories.AuthorRepository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Authors>> GetAllAsync();
        Task<Authors?> GetByIdAsync(int id);
        Task<IEnumerable<Authors>> FindByNameAsync(string? name);
        Task<Authors> AddAsync(Authors author);
        Task<bool> UpdateAsync(Authors author);
        Task<bool> DeleteAsync(int id);

    }
}
