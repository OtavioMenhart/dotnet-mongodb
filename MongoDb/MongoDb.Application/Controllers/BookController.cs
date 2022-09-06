using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDb.Domain.Dto;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDb.Domain.Interfaces.Services;

namespace MongoDb.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IBookRepository _bookRepository;

        public BookController(IDataService dataService)
        {
            _dataService = dataService;
            _bookRepository = _dataService.Book;
        }

        [HttpPost]
        public async Task<IActionResult> Post( CreateOrUpdateBookDto book)
        {
            try
            {
                await _bookRepository.CreateBookAsync(book);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
