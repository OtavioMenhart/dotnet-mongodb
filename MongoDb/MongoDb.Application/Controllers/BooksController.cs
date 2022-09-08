using Microsoft.AspNetCore.Mvc;
using MongoDb.Domain.Dto;
using MongoDb.Domain.Interfaces.Services;
using System.Net;

namespace MongoDb.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrUpdateBookDto book)
        {
            try
            {
                await _bookService.CreateBookAsync(book);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(string bookId)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(bookId);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var books = _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (KeyNotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string bookId)
        {
            try
            {
                await _bookService.DeleteBookAsync(bookId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(CreateOrUpdateBookDto bookUpdate, string bookId)
        {
            try
            {
                await _bookService.UpdateBookAsync(bookId, bookUpdate);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
