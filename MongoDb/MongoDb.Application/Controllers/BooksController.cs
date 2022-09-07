using Microsoft.AspNetCore.Mvc;
using MongoDb.Domain.Dto;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDb.Domain.Interfaces.Services;
using System.Net;

namespace MongoDb.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IBookRepository _bookRepository;

        public BooksController(IDataService dataService)
        {
            _dataService = dataService;
            _bookRepository = _dataService.Book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrUpdateBookDto book)
        {
            try
            {
                await _bookRepository.CreateBookAsync(book);
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
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                    return NoContent();

                return Ok(book);
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
                var books = _bookRepository.GetAll();
                if (books.Count() == 0)
                    return NoContent();

                return Ok(books);
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
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                    return NoContent();

                await _bookRepository.DeleteBookAsync(book.Id);
                return Ok();
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
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                    return NoContent();

                await _bookRepository.UpdateBookAsync(bookId, bookUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
