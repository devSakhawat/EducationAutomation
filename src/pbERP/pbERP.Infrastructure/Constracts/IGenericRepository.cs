using pbERP.Infrastructure.Specifications;
using System.Linq.Expressions;

namespace pbERP.Infrastructure.Constracts;

public interface IGenericRepository<T> where T : class
{
   Task<T> GetByKeyAsync(int key);
   Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
   //Task<IReadOnlyList<T>> ListAsync();
   //Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter);
   //Task<IReadOnlyList<T>> ListAsyncDesc(Expression<Func<T, long>> predicate);

   Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, long>>? predicate = null, Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

   Task<T> GetByKeyWithSpec(ISpecification<T> spec);
   Task<IReadOnlyList<T>> ListAsyncWithSpec(ISpecification<T> spec);
   Task<int> CountAsync(ISpecification<T> spec);

   void AddAsync(T entity);
   void UpdateEntity(T entity);
   void DeleteEntity(T entity);
   Task<bool> IsDuplicate(Expression<Func<T, bool>> filter);
   Task<bool> IsExists(Expression<Func<T, bool>> expression);
   Task<T> GetLastRecord(string columnName);
   Task<long> GetNextId(string columnName);
}