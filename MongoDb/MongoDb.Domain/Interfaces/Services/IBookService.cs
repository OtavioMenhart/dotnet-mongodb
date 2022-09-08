using MongoDb.Domain.Dto;
using MongoDb.Domain.Entities;

namespace MongoDb.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<Book> GetBookByIdAsync(string bookId);
        Task CreateBookAsync(CreateOrUpdateBookDto model);
        Task UpdateBookAsync(string bookId, CreateOrUpdateBookDto model);
        Task DeleteBookAsync(string bookId);
        IEnumerable<Book> GetAllBooksAsync();
    }
}
