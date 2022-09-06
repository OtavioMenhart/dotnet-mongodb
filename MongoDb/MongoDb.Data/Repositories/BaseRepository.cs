using MongoDb.Domain.Entities;
using MongoDb.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoDb.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;
        public BaseRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(database.DatabaseNamespace.DatabaseName);
        }
        public async Task AddAsync(T obj)
        {
            await _collection.InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _collection.DeleteOneAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _collection.AsQueryable();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var filter = Builders<T>.Filter.Where(predicate);
            return (await _collection.FindAsync(filter)).FirstOrDefault();
        }

        public async Task UpdateAsync(T obj)
        {
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", obj.Id), obj);
        }
    }
}
