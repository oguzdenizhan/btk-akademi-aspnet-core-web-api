﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repostories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]

        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)

        {
            try
            {
                //LINQ
                var book = _repositoryContext
                    .Books
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if (book is null)
                {
                    return NotFound();
                }
                return Ok(book);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();//400
                }
                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //check book
                var entity = _repositoryContext
                    .Books
                    .Where(b => b.Id.Equals(book.Id))
                    .SingleOrDefault();
                if (entity is null)
                    return NotFound(); //404
                //check id
                if (id != book.Id)
                    return BadRequest(); //400

                entity.Title = book.Title;
                entity.Price = book.Price;
                _repositoryContext.SaveChanges();
                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            var entity = _repositoryContext
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id= {id} could not found!"
                }); //404
            }
            _repositoryContext.Books.Remove(entity);
            _repositoryContext.SaveChanges(); 
            return NoContent(); //204
        }
        
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            //check entity
            var entity = _repositoryContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
            if (entity is null)
            {
                return NotFound(); //404
            }
            bookPatch.ApplyTo(entity);
            _repositoryContext.SaveChanges();
            return NoContent();//204



        }
        
    }
}
