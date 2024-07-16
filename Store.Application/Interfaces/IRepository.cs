using System.Linq.Expressions;

namespace Store.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string[]? includeProperties = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
    }
}
