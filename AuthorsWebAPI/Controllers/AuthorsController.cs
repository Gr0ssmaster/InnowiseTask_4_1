using AuthorsWebAPI.Models;
using AuthorsWebAPI.Services.AuthorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorsWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_authorService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var author = _authorService.GetById(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Authors author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _authorService.Add(author);
            if (!result.Success)
            {
                return BadRequest(new {error = result.Error});
            }
            return CreatedAtAction(nameof(Get), new {id = result.author!.Id}, result.author);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Authors author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _authorService.Update(id, author);
            return success ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var success = _authorService.Delete(id);
            return success ? NotFound() : NoContent();
        }

    }
}
