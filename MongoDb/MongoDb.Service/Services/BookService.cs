using FluentValidation;
using MongoDb.Domain.Dto;
using MongoDb.Domain.Entities;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDb.Domain.Interfaces.Services;
using Serilog;

namespace MongoDb.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IDataService _dataService;
        private readonly IValidator<CreateOrUpdateBookDto> _validator;
        private readonly IBookRepository _bookRepository;

        public BookService(IDataService dataService, IValidator<CreateOrUpdateBookDto> validator)
        {
            _dataService = dataService;
            _bookRepository = _dataService.Book;
            _validator = validator;
        }

        public async Task CreateBookAsync(CreateOrUpdateBookDto model)
        {
            await _validator.ValidateAndThrowAsync(model);

            Book book = new Book
            {
                Name = model.Name,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Price = model.Price
            };

            await _bookRepository.CreateBookAsync(book);
        }

        public async Task DeleteBookAsync(string bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException($"Book is null - id: {bookId}");

            await _bookRepository.DeleteBookAsync(book.Id);
        }

        public IEnumerable<Book> GetAllBooksAsync()
        {
            var books = _bookRepository.GetAll();
            if (books.Count() == 0)
                throw new KeyNotFoundException("No records in database");
            return books;
        }

        public async Task<Book> GetBookByIdAsync(string bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException($"Book is null - id: {bookId}");

            Log.Information($"Book Id found", book);
            return book;
        }

        public async Task UpdateBookAsync(string bookId, CreateOrUpdateBookDto model)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException($"Book is null - id: {bookId}");

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
        }
    }
}
