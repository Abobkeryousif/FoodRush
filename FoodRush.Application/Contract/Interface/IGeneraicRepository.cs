namespace FoodRush.Application.Contract.Interface
{
    public interface IGeneraicRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string[] include = null);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string[] include = null);

        IQueryable<T> Sync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string[] include = null);
        Task<T> GetByIdAsync(int Id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);

        Task<bool> AddRange(List<T> entity);

        Task<bool> IsExist(Expression<Func<T, bool>> filter);
    }
}
