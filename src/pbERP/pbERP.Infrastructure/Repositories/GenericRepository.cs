using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Specifications;
using System.Linq.Expressions;

namespace pbERP.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
   protected readonly pbERPContext context;
   internal DbSet<T> dbSet;

   public GenericRepository(pbERPContext _context)
   {
      context = _context;
      dbSet = context.Set<T>();
   }

   public async Task<T> GetByKeyAsync(int key)
   {
      return await dbSet.FindAsync(key);
   }

   public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
   {
      IQueryable<T> query = dbSet.Where(filter);
      return await query.FirstOrDefaultAsync();
   }

   //public async Task<IReadOnlyList<T>> ListAsync()
   //{
   //   return await dbSet.ToListAsync();
   //}

   /// <summary>
   /// Mehtod with single where condition.
   /// </summary>
   /// <param name="filter"></param>
   /// <returns>List of filtered record</returns>
   
   //public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter)
   //{
   //   return await dbSet.Where(filter).ToListAsync();
   //}

   //public async Task<IReadOnlyList<T>> ListAsyncDesc(Expression<Func<T, long>> predicate)
   //{
   //   return await dbSet.OrderByDescending(predicate).ToListAsync();
   //}


   // IncludeProp - "Navigation1,2, 3"
   public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, long>>? predicate = null, Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
   {
      IQueryable<T> query = dbSet;
      query = (filter != null) ? query.Where(filter).AsQueryable().AsNoTracking() : query.AsQueryable().AsNoTracking();
      query = includeProperties != null ? includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProp) => current.Include(includeProp.Trim())) : query;
      
      query = (predicate != null) ? query.OrderByDescending(predicate) : query;
      return await query.ToListAsync();
   }

   public async Task<T> GetByKeyWithSpec(ISpecification<T> spec)
   {
      return await ApplySpecification(spec).FirstOrDefaultAsync();
   }

   public async Task<IReadOnlyList<T>> ListAsyncWithSpec(ISpecification<T> spec)
   {
      return await ApplySpecification(spec).ToListAsync();
   }

   public async Task<int> CountAsync(ISpecification<T> spec)
   {
      return await ApplySpecification(spec).CountAsync();
   }

   // ApplySpecification Method
   private IQueryable<T> ApplySpecification(ISpecification<T> spec)
   {
      return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
   }

   public void AddAsync(T entity)
   {
      dbSet.AddAsync(entity);
   }

   public void UpdateEntity(T entity)
   {
      context.Entry(entity).State = EntityState.Modified;
      dbSet.Update(entity);
   }

   public void DeleteEntity(T entity)
   {
      context.Entry(entity).State = EntityState.Modified;
      dbSet.Remove(entity);
   }

   public async Task<bool> IsDuplicate(Expression<Func<T, bool>> filter)
   {
      var query = await dbSet.AsQueryable().Where(filter).FirstOrDefaultAsync();

      if (query != null)
         return true;
      return false;
   }

   public async Task<bool> IsExists(Expression<Func<T, bool>> expression)
   {
      return await dbSet.AnyAsync(expression);
   }

   //public async Task<T> GetLastRecord(Expression<Func<T, bool>> filter)
   //{
   //   T lastEntity = await dbSet.AsQueryable().OrderByDescending(filter).FirstOrDefaultAsync();
   //   if (lastEntity == null)
   //   {
   //      return default(T);
   //   }

   //   return lastEntity;
   //}

   public async Task<T> GetLastRecord(string columnName)
   {
      T lastEntity = await dbSet.OrderByDescending(e => EF.Property<long>(e, columnName)).FirstOrDefaultAsync();
      if (lastEntity == null)
      {
         return default(T);
      }

      return lastEntity;
   }

   public async Task<long> GetNextId(string columnName)
   {
      var maxId = await dbSet.Select(e => EF.Property<long>(e, columnName))
                             .DefaultIfEmpty()
                             .MaxAsync();
      return (maxId == 0) ? 1 : maxId + 1;
   }

}
