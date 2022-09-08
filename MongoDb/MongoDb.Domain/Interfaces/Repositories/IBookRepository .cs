using MongoDb.Domain.Entities;

namespace MongoDb.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(string bookId);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(string bookId, Book book);
        Task DeleteBookAsync(string bookId);
    }
}
