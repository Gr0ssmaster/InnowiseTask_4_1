using AuthorsWebAPI.Models;
using AuthorsWebAPI.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace AuthorsWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService service)
        {
            _bookService = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_bookService.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Books book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _bookService.Add(book);
            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(nameof(Get), new { id = result.book!.Id }, result.book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Books book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _bookService.Update(id, book);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _bookService.Delete(id);
            return success ? NoContent() : NotFound();
        }
    }

}

