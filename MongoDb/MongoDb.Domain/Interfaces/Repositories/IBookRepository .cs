using MongoDb.Domain.Dto;
using MongoDb.Domain.Entities;

namespace MongoDb.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(string bookId);
        Task CreateBookAsync(CreateOrUpdateBookDto model);
        Task UpdateBookAsync(string id, CreateOrUpdateBookDto model);
        Task DeleteBookAsync(string id);
    }
}
