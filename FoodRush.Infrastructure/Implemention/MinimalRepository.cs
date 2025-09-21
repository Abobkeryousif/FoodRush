
namespace FoodRush.Infrastructure.Implemention
{
    public class MinimalRepository<T> : IMinimalRepository<T> where T : class
    {
        private readonly ApplicationDbContaxt _context;

        public MinimalRepository(ApplicationDbContaxt context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string[] include = null)
        {
            var result = Sync(filter, OrderBy, include);
            return result.FirstOrDefaultAsync();
        }

        public IQueryable<T> Sync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
                query = query.Where(filter);
            if (include != null)
                foreach (var item in include)
                    query = query.Include(item);
            if (OrderBy != null)
                OrderBy(query);
            return query;

        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
    }
}
