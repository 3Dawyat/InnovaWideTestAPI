
using InnovaWideTest.Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InnovaWideTest.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (withNoTracking)
                query = query.AsNoTracking();

            if (include is not null)
                query = include(query);

            return await query.FirstOrDefaultAsync();
        }
        public T? GetById(int id) => _context.Set<T>().Find(id);
        public IQueryable<T> AsQueryable()
      => _context.Set<T>().AsQueryable();
        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);
        public async Task<T?> GetByIdAsync(int id)
       => await _context.Set<T>().FindAsync(id);
        public async Task DeleteAsync(int id)
        {
            var oT = await _context.Set<T>().FindAsync(id);
            if (oT != null)
                _context.Set<T>().Remove(oT);
        }
        public async Task AddAsync(List<T> entity)
            => await _context.Set<T>().AddRangeAsync(entity);


        public void UpdateRange(List<T> entity)
        => _context.Set<T>().UpdateRange(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Detach(T entity)
         => _context.Entry(entity).State = EntityState.Detached;


        public async Task<List<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

        public bool IsExist(Expression<Func<T, bool>> Criteria)
        => _context.Set<T>().Any(Criteria);

        public async Task<bool> SaveChangesAsync()
        => await _context.SaveChangesAsync() > 1;

    }
}
