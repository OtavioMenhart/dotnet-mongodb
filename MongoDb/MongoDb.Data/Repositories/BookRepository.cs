using MongoDb.Domain.Dto;
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

        public async Task CreateBookAsync(CreateOrUpdateBookDto model)
        {
            Book book = new Book
            {
                Name = model.Name,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Price = model.Price
            };

            await AddAsync(book);
        }

        public async Task UpdateBookAsync(string id, CreateOrUpdateBookDto model)
        {
            Book book = new Book
            {
                Id = id,
                Name = model.Name,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Price = model.Price
            };

            await UpdateAsync(book);
        }

        public async Task DeleteBookAsync(string id)
        {
            await DeleteAsync(x => x.Id == id);
        }
    }
}
