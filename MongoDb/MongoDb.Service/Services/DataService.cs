using MongoDb.Data.Context;
using MongoDb.Data.Repositories;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDb.Domain.Interfaces.Services;

namespace MongoDb.Service.Services
{
    public class DataService : IDataService
    {
        private readonly MongoDbContext _dbContext;

        public DataService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBookRepository Book => new BookRepository(_dbContext.Database);
    }
}
