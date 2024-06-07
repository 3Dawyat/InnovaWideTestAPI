using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InnovaWideTest.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T table);

        Task AddAsync(List<T> table);

        void Update(T table);
        void UpdateRange(List<T> table);
        void Detach(T entity);
        Task<List<T>> GetAllAsync();
        void Attach(T entity);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> AsQueryable();
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    }
}
