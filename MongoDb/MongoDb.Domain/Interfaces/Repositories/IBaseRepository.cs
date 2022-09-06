using System.Linq.Expressions;

namespace MongoDb.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
