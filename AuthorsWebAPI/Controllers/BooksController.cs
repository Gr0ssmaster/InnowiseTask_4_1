using AuthorsWebAPI.Models;
using AuthorsWebAPI.Repositories.BookRepository;
using AuthorsWebAPI.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorsWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _bookService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }
        [HttpGet("after/{year}")]
        public async Task<IActionResult> GetAfterYear(int year) {
            var books = await _bookService.GetBooksAfterYearAsync(year);
            return Ok(books);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Books book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var (Success, Error, Created) = await _bookService.AddAsync(book);
            if(!Success) return BadRequest(new {error = Error});

            return CreatedAtAction(nameof(Get), new {id = Created!.Id}, Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Books book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _bookService.UpdateAsync(id, book);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }

}

