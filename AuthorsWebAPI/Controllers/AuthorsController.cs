using AuthorsWebAPI.Models;
using AuthorsWebAPI.Repositories.AuthorRepository;
using AuthorsWebAPI.Services.AuthorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorsWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService) => _authorService = authorService;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _authorService.GetAllAsync());
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var author = await _authorService.GetByIdAsync(id);
            return author == null ? NotFound() : Ok(author);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name) {
            var authors = await _authorService.FindByNameAsync(name);
            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Authors author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (Success, Error, Created) = await _authorService.AddAsync(author);
            if(!Success) return BadRequest(new {error = Error});
            return CreatedAtAction(nameof(Get), new { id = Created!.Id }, Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Authors author)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
                
            var success = await _authorService.UpdateAsync(id, author);
            return success ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var success = await _authorService.DeleteAsync(id);
            return success ? NoContent(): NotFound();
        }

    }
}
