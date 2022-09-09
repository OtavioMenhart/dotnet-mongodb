using FluentValidation;
using Microsoft.Extensions.Logging;
using MongoDb.Domain.Dto;
using MongoDb.Domain.Entities;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDb.Domain.Interfaces.Services;

namespace MongoDb.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IDataService _dataService;
        private readonly IValidator<CreateOrUpdateBookDto> _validator;
        private ILogger<BookService> _logger;
        private readonly IBookRepository _bookRepository;

        public BookService(IDataService dataService, IValidator<CreateOrUpdateBookDto> validator, ILogger<BookService> logger)
        {
            _dataService = dataService;
            _bookRepository = _dataService.Book;
            _validator = validator;
            _logger = logger;
        }

        public async Task CreateBookAsync(CreateOrUpdateBookDto model)
        {
            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                var exception = new ValidationException(validation.Errors);
                _logger.LogError(exception, $"Validation failed: {model.Name}");
                throw exception;
            }

            Book book = new Book
            {
                Name = model.Name,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Price = model.Price
            };

            await _bookRepository.CreateBookAsync(book);
            _logger.LogInformation($"Created: {model.Name}");
        }

        public async Task DeleteBookAsync(string bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                ThrowAndLogKeyNotFoundException(bookId);

            await _bookRepository.DeleteBookAsync(book.Id);
            _logger.LogInformation($"Deleted: {bookId}");
        }

        public IEnumerable<Book> GetAllBooksAsync()
        {
            var books = _bookRepository.GetAll();
            if (books.Count() == 0)
                ThrowAndLogKeyNotFoundException();
            _logger.LogInformation("Books found", books);
            return books;
        }

        public async Task<Book> GetBookByIdAsync(string bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                ThrowAndLogKeyNotFoundException(bookId);

            _logger.LogInformation($"Book Id found", book);
            return book;
        }

        public async Task UpdateBookAsync(string bookId, CreateOrUpdateBookDto model)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                ThrowAndLogKeyNotFoundException(bookId);

            await _validator.ValidateAndThrowAsync(model);

            Book bookUpdate = new Book
            {
                Id = bookId,
                Name = model.Name,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Price = model.Price
            };
            await _bookRepository.UpdateBookAsync(bookId, bookUpdate);
            _logger.LogInformation("Book updated", bookUpdate);
        }

        private void ThrowAndLogKeyNotFoundException(string bookId)
        {
            var exception = new KeyNotFoundException($"Book is null - id: {bookId}");
            _logger.LogError(exception, $"BookId {bookId} not found");
            throw exception;
        }

        private void ThrowAndLogKeyNotFoundException()
        {
            var exception = new KeyNotFoundException("No records in database");
            _logger.LogError(exception, "No records in database");
            throw exception;
        }
    }
}
