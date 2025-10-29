using AuthorsWebAPI.Data;
using AuthorsWebAPI.Models;
using AuthorsWebAPI.Repositories.AuthorRepository;
using AuthorsWebAPI.Resources;
using System.Threading.Tasks;

namespace AuthorsWebAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
             _authorRepository = authorRepository;
        }   

        public async Task<IEnumerable<Authors>> GetAllAsync() =>
            await _authorRepository.GetAllAsync();
      
        public async Task<Authors> GetByIdAsync(int id)=>
            await _authorRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Authors>> FindByNameAsync(string name) =>
            await _authorRepository.FindByNameAsync(name);
        public async Task<(bool Success, string? Error, Authors? author)> AddAsync(Authors author)
        { 
            if (string.IsNullOrWhiteSpace(author.Name))
            {
                return (false, ValidationMessages.AuthorNameRequired, null);
            }

            if (author.DateOfBirth == default)
            {
                return (false, ValidationMessages.AuthorBirthRequired, null);
            }

            var created = await _authorRepository.AddAsync(author);
            return (true, null, created);
        }

        public async Task<bool> UpdateAsync(int id, Authors author)
        {
            var existing = await _authorRepository.GetByIdAsync(id);
            if (existing == null) {
                return false;
            }
            author.Id = id;
            return await _authorRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _authorRepository.DeleteAsync(id);
    }
}
