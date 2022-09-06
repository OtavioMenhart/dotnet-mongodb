using MongoDb.Domain.Interfaces.Repositories;

namespace MongoDb.Domain.Interfaces.Services
{
    public interface IDataService
    {
        public IBookRepository Book { get; }
    }
}
