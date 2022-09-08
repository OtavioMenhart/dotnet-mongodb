using MongoDb.Domain.Entities;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace MongoDb.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<Book> GetBookByIdAsync(string bookId)
        {
            return await GetSingleAsync(x => x.Id == bookId);
        }

        public async Task CreateBookAsync(Book book)
        {
            await AddAsync(book);
        }

        public async Task UpdateBookAsync(string bookId, Book book)
        {
            await UpdateAsync(book);
        }

        public async Task DeleteBookAsync(string bookId)
        {
            await DeleteAsync(x => x.Id == bookId);
        }
    }
}
