using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]

        public IActionResult GetAllBooks()
        {
            var books = _manager.bookService.GetAllBooks(false);
            return Ok(books);

        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)

        {
            //LINQ
            var book = _manager
                .bookService.GetOneBook(id, false);


            return Ok(book);

        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            if (book is null)
            {
                return BadRequest();//400
            }
            _manager.bookService.CreateOneBook(book);

            return StatusCode(201, book);

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            if (bookDto is null)
            {
                return BadRequest();//400
            }
            //check book
            _manager.bookService.UpdateOneBook(id, bookDto, true);
            return NoContent(); //204
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _manager.bookService.DeleteOneBook(id, false);
            return NoContent(); //204
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            //check entity
            var entity = _manager
                .bookService
                .GetOneBook(id, true);

            bookPatch.ApplyTo(entity);
            _manager.bookService.UpdateOneBook(id, new BookDtoForUpdate(entity.Id,entity.Title,entity.Price), true);
            return NoContent();//204
        }

    }
}
