﻿using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var books = _manager.bookService.GetAllBooks(false);
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
                var book = _manager
                    .bookService.GetOneBook(id, false);

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
                _manager.bookService.CreateOneBook(book);

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
                if (book is null)
                {
                    return BadRequest();//400
                }
                //check book
                _manager.bookService.UpdateOneBook(id, book, true);
                return NoContent(); //204
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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

            if (entity is null)
            {
                return NotFound(); //404
            }
            bookPatch.ApplyTo(entity);
            _manager.bookService.UpdateOneBook(id, entity, true);
            return NoContent();//204



        }

    }
}
